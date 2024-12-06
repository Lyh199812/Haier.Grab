using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.IBLL;
using HalconDotNet;
using HVisionLibs.Core.Preprocessing;
using HVisionLibs.Core.TemplateMatch.OCR;
using HVisionLibs.Core.TemplateMatch;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using HVisionLibs.Shared.Controls;
using HVisionLibs.Core;
using System.Windows;

namespace Base.Clent.HVisionModule.Preprocessing.ViewModels
{
    public class ZoomFactorViewModel : PageViewModelBase
    {
        public ZoomFactorService Service {  get; set; }
        public ZoomFactorViewModel(IUnityContainer unityContainer, IRegionManager regionManager, ZoomFactorService mirrorService, IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL)
    : base(unityContainer, regionManager)
        {
            this.Service = mirrorService;
            this.PageTitle = "缩放因子设置";

            LoadImageCommand=new DelegateCommand(LoadImage);
            RunCommand = new DelegateCommand(Run);
            SaveImageCommand=new DelegateCommand(SaveImage);
        }

        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand LoadImageCommand { get; set; }
        public DelegateCommand SaveImageCommand { get; set; }


        private HOperateResult operateResult;

        public HOperateResult OperateResult
        {
            get { return operateResult; }
            set { operateResult = value; RaisePropertyChanged(); }
        }

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 加载图像
        /// </summary>
        private void LoadImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = (bool)dialog.ShowDialog();
            if (result)
            {
                var img = new HImage();
                img.ReadImage(dialog.FileName);
                Image = img;
            }
        }

        public void SaveImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg|Bitmap Files (*.bmp)|*.bmp"; // 文件格式过滤
            saveFileDialog.Title = "保存图像";

            if (saveFileDialog.ShowDialog() == true) // 用户选择了文件路径
            {
                OperateResult = Service.SaveImage(Image, saveFileDialog.FileName);
            }
        }


      

        /// <summary>
        /// 执行
        /// </summary>
        private void Run()
        {
            OperateResult = Service.Run(ref image);
            Image = image;
        }


    }
}
