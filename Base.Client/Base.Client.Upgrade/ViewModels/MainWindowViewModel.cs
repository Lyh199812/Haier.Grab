using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Upgrade.DataAccess;
using Base.Client.Upgrade.Models;

namespace Base.Client.Upgrade.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Action ConfirmAction;

        public ObservableCollection<FileModel> Files { get; set; } = new ObservableCollection<FileModel>();

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }




        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public MainWindowViewModel(string files)
        {
            string[] fileArray = files.Split(';');
            // 显示待更新列表
            for (int i = 0; i < fileArray.Length; i++)
            {
                string[] fileInfo = fileArray[i].Split('|');
                if (fileInfo.Length == 4)
                {
                    Files.Add(new FileModel
                    {
                        Index = i + 1,
                        FileName = fileInfo[0],
                        FilePath = fileInfo[1],
                        FileMd5 = fileInfo[2],
                        FileLenght = int.TryParse(fileInfo[3], out int len) ? len : 0,
                        State = "待更新",
                    });
                }
            }



        }


        private ICommand _startCommand;

        public ICommand StartCommand
        {
            get
            {
                if (_startCommand == null)
                    _startCommand = new DelegateCommand(OnStart);// 如果列表中有项，才可以执行更新

                return _startCommand;
            }
        }

        private void OnStart()
        {
            // 请求API进行文件下载
            if (Files.Count > 0)
            {
                //Task.Run(async () =>
                //{
                //    int count = await webDataAccess.DownloadAsync(
                //          FileList.ToList(),
                //          new Action<FileInfoModel>((fi) =>
                //          {
                //              completedCount++;
                //              Progress = (int)(completedCount * 1.0 / FileList.Count * 100);
                //          }));


                //    if (FileList.Count != count)
                //        IsCanStart = true;
                //    else
                //    {
                //        // 打开更新完成确认窗口
                //        ConfirmAction?.Invoke();
                //    }

                //});

                Task.Run(() =>
                {
                    Files.ToList().ForEach(file =>
                    {
                        file.State = "正在更新";
                        WebDataAccess webDataAccess = new WebDataAccess();
                        webDataAccess.DownloadCompleted = new Action(() =>
                        {
                            // 将下载的文件信息写入本地数据库（缓存，后续做对比）
                            // 异常逻辑：
                            if (!new LocalDataAccess().UpdateFileInfo(file.FileName, file.FileMd5, file.FileLenght))
                            {
                                file.ErrorMsg = "缓存版本更新失败";
                                file.State = "更新失败";
                            }

                            file.Progress = 0;
                            file.State = "已更新";
                            // 下载成功
                            autoResetEvent.Set();
                        });

                        webDataAccess.DownloadPrograssChanged = new Action<int>(progress =>
                        {
                            // 每次的下载 进度
                        });
                        webDataAccess.DownloadAsync(file, ".\\" + Path.Combine(file.FilePath, file.FileName));
                        autoResetEvent.WaitOne();
                    });

                    // 走到这里说明下载完成

                    // 提示   更新成功，是否运行主程序 
                    ConfirmAction?.Invoke();
                });
            }
        }
    }
}
