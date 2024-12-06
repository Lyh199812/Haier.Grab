using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Unity;
using Base.Client.Common;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.Models;

namespace Base.Client.ViewModels
{
    public class WinLoginViewModel : BindableBase
    {
        public UserModel UserInfo { get; set; } = new UserModel();

        ILoginBLL _loginBLL;//
        IFileBLL _fileBLL;

        [Dependency]
        public Dispatcher MainDispatcher { get; set; }
        [Dependency]
        public GlobalValue _globalValue { get; set; }

        // 当打开登录窗口的时候    执行构造函数
        public WinLoginViewModel(ILoginBLL loginBLL, IFileBLL fileBLL, IEventAggregator ea)
        {
            _loginBLL = loginBLL;
            _fileBLL = fileBLL;
            // 1、登录成功-》关闭当前窗口
            // 2、登录失败-显示一下错误信息
            LoginCommand = new DelegateCommand<object>(DoLogin);

            
            // 开始检查程序版本
            this.IsLoading = true;
            this.LoadingMessage = "正在检查软件版本";
            Task.Run(async () =>
            {
                //await Task.Delay(10000);   // Task  只是一个任务   -》方法

                // 从服务端获取更新文件的列表
                try
                {
                    List<UpgradeFileModel> fileList_server = await _fileBLL.GetServerFileList();
                    if (fileList_server?.Count > 0)
                    {
                        // 做版本比对
                        // 从本
                        List<UpgradeFileModel> fileList_local = await _fileBLL.GetLocalFileList();

                        // 1、本地列表中有，并且服务列表中也有：MD5不一样的文件留下，这些文件将进行更新覆盖
                        // 2、本地列表中有，并且服务列表中没有：删除本地文件（可参照、选做）
                        // 3、本地列表中没有，并且服务列表中有：服务列表中的文件全部下载覆盖


                        List<UpgradeFileModel> upgradeFileList = new List<UpgradeFileModel>();
                        fileList_server.ForEach(fileRow =>
                        {
                            bool existState =
                            //1、本地列表中有，并且服务列表中也有：MD5不一样的文件留下，这些文件将进行更新覆盖
                            fileList_local.Exists(fl => fl.FileName == fileRow.FileName && fl.FileMd5 != fileRow.FileMd5)

                            ||
                            //3、本地列表中没有，并且服务列表中
                            !fileList_local.Exists(fl => fl.FileName == fileRow.FileName);

                            // 两个条件任意成立 ，都需要进行服务端文件列表中当前文件的更新
                            if (existState)
                            {
                                upgradeFileList.Add(fileRow);
                            }
                        });

                        if (upgradeFileList.Count > 0)
                        {
                            // 启动更新程序  ，并且将需要更新的数据发送给更新程序
                            string updateListJson = String.Join(";", upgradeFileList.Select(fi => fi.FileName + "|" + fi.FilePath + "|" + fi.FileMd5 + "|" + fi.Length).ToList());

                            // Base.Client.upgrade.exe updatelistJson
                            // List
                            // filename|sefsef/sefsef/sef.dll|23425232432|2343242
                            // filename|sefsef/sefsef/sef.dll|23425232432|2343242

                            // filename|sefsef/sefsef/sef.dll|23425232432|2343242;filename|sefsef/sefsef/sef.dll|23425232432|2343242

                            Process process = Process.Start(
                                "Base.Client.Upgrade.exe",
                                updateListJson
                                );
                            process.WaitForInputIdle();

                            // 操作关闭窗口
                            ea.GetEvent<CloseWindowEvent>().Publish();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = ex.ToString();
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private string _loadingMessage;
        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { SetProperty(ref _loadingMessage, value); }
        }


        // 必要的登录过程
        public ICommand LoginCommand { get; set; }

        private void DoLogin(object obj)
        {
            if (string.IsNullOrEmpty(UserInfo.UserName))
            {
                this.ErrorMessage = "请输入用户名";
                return;
            }
            if (string.IsNullOrEmpty(UserInfo.Password))
            {
                this.ErrorMessage = "请输入密码";
                return;
            }

            // 打开Loading动画 
            this.LoadingMessage = "正在登录....";
            this.IsLoading = true;
            Task.Run(async () =>
            {
                //await Task.Delay(3000);
                try
                {
                    var user = await _loginBLL.Login(UserInfo.UserName, UserInfo.Password);
                    if (user != null)
                    {
                        // 将用户信息保存在内存中
                        _globalValue.UserInfo = user;
                        _globalValue.IsLocalMode = false;
                        // 对接 
                        // 登录成功   请求Api    耗时操作    Loading动画显示

                        MainDispatcher.Invoke(() =>
                        {
                            (obj as Window).DialogResult = true;// 关闭登录   进入主窗口    必须是UI线程
                        });
                    }
                    else
                    {
                        throw new Exception("登录失败，未知错误");
                    }
                }
                catch (Exception ex)
                {
                    // 异常信息  在界面显示
                    ErrorMessage = ex.ToString();
                }
                finally
                {
                    // 关闭Loading动画
                    this.IsLoading = false;
                }
            });
        }
    }
}
