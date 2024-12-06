using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class FileUploadViewModel : PageViewModelBase
    {
        //private string _pageTitle = "更新文件上传";
        //public string PageTitle
        //{
        //    get { return _pageTitle; }
        //    set { SetProperty(ref _pageTitle, value); }
        //}
        //public bool IsCanClose { get; set; } = true;

        //public string AddButtonText { get; set; } = "上传";



        //private string _keyword;
        //public string Keyword
        //{
        //    get { return _keyword; }
        //    set { SetProperty(ref _keyword, value); }
        //}



        // 具体业务有关
        public ObservableCollection<FileInfoModel> Files { get; set; } = new ObservableCollection<FileInfoModel>();


        IFileBLL _fileBLL;
        IUnityContainer _unityContainer;
        IDialogService _dialogService;
        public FileUploadViewModel(
            IUnityContainer unityContainer, IFileBLL fileBLL,
            IDialogService dialogService, IRegionManager regionManager)
            : base(unityContainer, regionManager)
        {
            //this.NavAddress = "fuv";    // 最简单的处理方式

            // 初始参数
            this.PageTitle = "更新文件上传";
            this.AddButtonText = "上传";

            _unityContainer = unityContainer;
            _fileBLL = fileBLL;
            _dialogService = dialogService;

            //CloseCommand = new DelegateCommand(() =>
            //{
            //    // 区域 对应的一个View列表
            //    var obj = unityContainer.Registrations.Where(v => v.Name == "fuv").FirstOrDefault();
            //    string name = obj.MappedToType.Name;


            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        var region = regionManager.Regions["MainViewRegion"];
            //        var view = region.Views.Where(v => v.GetType().Name == name).FirstOrDefault();
            //        if (view != null)
            //            region.Remove(view);
            //    }
            //});

            //RefreshCommand = new DelegateCommand(Refresh);
            //AddCommand = new DelegateCommand(Add);

            Refresh();
        }

        // 处理输入的按键的类型
        //public void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        this.Refresh();
        //    }
        //}

        //public ICommand CloseCommand { get; set; }
        //public ICommand RefreshCommand { get; set; }
        //public ICommand AddCommand { get; set; }// 在这个页面这里表示上传动作

        // 多态
        public override void Refresh()
        {
            // 
            Task.Run(async () =>
            {
                // 应用关键词查找的话
                var files = await _fileBLL.GetServerFileList(this.Keyword);
                _unityContainer.Resolve<Dispatcher>().Invoke(() =>
                {
                    Files.Clear();
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        FileInfoModel model = new FileInfoModel();
                        model.Index = i + 1;
                        model.FileName = file.FileName;
                        model.UploadTime = file.UploadTime;
                        model.FilePath = file.FilePath;
                        // 在这里进行删除的时候可以将集合中对应的子项删除
                        model.DeleteCommand = new DelegateCommand<FileInfoModel>(model =>
                        {
                            // 执行_fileBLL中的删除方法
                            // 根据FileName进行删除
                            // 服务端：除了删除数据库数据，删除目录下的文件
                        });
                        Files.Add(model);
                    }
                });

                //this.Keyword = "";
            });
        }

        public override void Add()
        {
            // 执行上传文件功能
            //1、打开上传文件弹出窗口

            _dialogService.ShowDialog("AddFileDialog");
        }
    }
}
