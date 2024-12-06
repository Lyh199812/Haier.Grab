using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddFileDialogViewModel : IDialogAware
    {
        // 
        public string Title => "系统更新文件上传";

        public event Action<IDialogResult> RequestClose2;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 接收参数
        }




        // 业务逻辑
        public ObservableCollection<FileInfoModel> FileList { get; set; } = new ObservableCollection<FileInfoModel>();

        public ICommand SelectFileCommand { get; set; }
        public ICommand UploadCommand { get; set; }

        public DialogCloseListener RequestClose { get; }

        IFileBLL _fileBll;
        public AddFileDialogViewModel(IFileBLL fileBll)
        {
            _fileBll = fileBll;

            SelectFileCommand = new DelegateCommand(DoSelectFile);
            UploadCommand = new DelegateCommand(DoUpload);
        }

        private void DoSelectFile()
        {
            // 打开选择文件对话框   openfiledialg
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true)
            {
                FileList.Clear();
                if (dialog.FileNames != null && dialog.FileNames.Length > 0)
                {
                    for (int i = 0; i < dialog.FileNames.Length; i++)
                    {
                        FileList.Add(new FileInfoModel
                        {
                            Index = i + 1,
                            FullPath = dialog.FileNames[i],// 当前选择文件的具体路径 
                            FileName = new FileInfo(dialog.FileNames[i]).Name,
                            State = "待上传"
                        });
                    }
                }
            }
        }


        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private void DoUpload()
        {
            // 服务端进行交互
            Task.Run(() =>
            {
                foreach (var item in FileList)
                {
                    _fileBll.UploadFile(
                            item.FullPath,// 本地文件全路径 
                            item.FilePath,// 更新子目录
                            value =>
                            {
                                // 进度值的变化 
                            },
                            () =>
                            {
                                // 上传结束的状态
                                autoResetEvent.Set();
                            }
                        );
                    autoResetEvent.WaitOne();
                }
            });
        }
    }
}
