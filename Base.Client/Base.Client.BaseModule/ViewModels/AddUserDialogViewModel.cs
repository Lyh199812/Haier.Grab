using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddUserDialogViewModel : BindableBase, IDialogAware
    {
        // 窗体的标题栏   窗口边框    
        public string Title
        {
            get; set;
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }


        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 接收参数  状态 ：新增/编辑     当前绑定的数据Model
            var mode = parameters.GetValue<int>("mode");
            if (mode == 0)
                Title = "添加新用户";
            else if (mode == 1)// 表示编辑 
            {
                UserInfo = parameters.GetValue<UserInfoModel>("data");
                Title = $"编辑用户信息[{UserInfo.UserName}]";
                IsReadOnlyUserName = true;
            }
        }



        private bool _isReadOnlyUserName = false;
        public bool IsReadOnlyUserName
        {
            get { return _isReadOnlyUserName; }
            set { SetProperty(ref _isReadOnlyUserName, value); }
        }
        public UserInfoModel UserInfo { get; set; } = new UserInfoModel()
        {
            UserIcon = "http://localhost:22643/api/file/img/1001.png" //"file/img/1001.png"
        };
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SelectIcon { get; set; }

        DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        string baseUrl = "http://localhost:22643/api/";

        IUserBLL _userBLL;
        public AddUserDialogViewModel(IUserBLL userBLL, IFileBLL fileBLL)
        {
            _userBLL = userBLL;


            ConfirmCommand = new DelegateCommand(async () =>
            {
                bool isNetFile = false;// 是否是网络文件

                string userIcon = UserInfo.UserIcon;
                if (userIcon.StartsWith(baseUrl))// 从数据库获取到用户信息后，然后进行编辑，不做图像修改的前提下
                {
                    isNetFile = true;
                    userIcon = UserInfo.UserIcon.Replace(baseUrl, "");
                }
                else
                {
                    FileInfo fi = new FileInfo(UserInfo.UserIcon);
                    userIcon = "file/img/" + UserInfo.UserName + fi.Extension;
                }
                // 新增的进候   得到的结果   d:\1001.png

                // 数据往数据库存
                var result = await userBLL.SaveUser(new Entity.UserEntity
                {
                    userId = UserInfo.UserId,
                    userName = UserInfo.UserName,
                    realName = UserInfo.RealName,
                    userIcon = userIcon,
                    age = UserInfo.Age,

                    state = 1
                });
                if (result.state != 200)
                {
                    var error = result.exceptionMessage;
                    System.Windows.MessageBox.Show(error);// 尽量统一
                    return;
                }

                if (!isNetFile)
                {
                    FileInfo fi = new FileInfo(UserInfo.UserIcon);
                    string fileName = UserInfo.UserName + fi.Extension;
                    // 将本地图像文件保存到服务器
                    fileBLL.UploadAvatar(
                        UserInfo.UserIcon,
                        fileName,
                        async () =>
                      {
                          // 关闭弹窗
                          DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                          RequestClose?.Invoke(dialogResult);
                      });
                }
                else
                {
                    // 关闭弹窗
                    DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                    RequestClose?.Invoke(dialogResult);
                }
            });



            CancelCommand = new DelegateCommand(() =>
            {
                // 关闭弹窗
                RequestClose?.Invoke(new DialogResult());
            });

            // 选择头像
            SelectIcon = new DelegateCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "*.*|";
                if (openFileDialog.ShowDialog() == true)
                {
                    var path = openFileDialog.FileName;
                    UserInfo.UserIcon = path;// 影响新增和编辑
                }
            });


            UserInfo.UniquenessCheck = new Func<object, bool>(CheckUserName);
        }

        private bool CheckUserName(object userName)
        {
            try
            {
                if (userName == null) return false;
                return _userBLL.CheckUserName(userName.ToString()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // 检查的过程出现异常
                return false;
            }
        }
    }
}
