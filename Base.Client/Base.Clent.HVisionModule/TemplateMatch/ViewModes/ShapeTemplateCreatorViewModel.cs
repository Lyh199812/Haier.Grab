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
using HVisionLibs.Core.TemplateMatch;

namespace Base.Clent.HVisionModule.TemplateMatch.ViewModels
{
    public class ShapeTemplateCreatorViewModel : PageViewModelBase
    {
        public ShapeTemplateCreatorService Service {  get; set; }
        public ShapeTemplateCreatorViewModel(IUnityContainer unityContainer, IRegionManager regionManager, ShapeTemplateCreatorService mirrorService, IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL)
    : base(unityContainer, regionManager)
        {
            this.Service = mirrorService;
            this.PageTitle = "Shape模板创建";
            DrawObjectList = new ObservableCollection<DrawingObjectInfo>();
            LoadImageCommand =new DelegateCommand(LoadImage);
            RunCommand = new DelegateCommand(Run);
            SaveImageCommand=new DelegateCommand(Save);
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


        private ObservableCollection<DrawingObjectInfo> drawObjectList;

        public ObservableCollection<DrawingObjectInfo> DrawObjectList
        {
            get { return drawObjectList; }
            set { drawObjectList = value; RaisePropertyChanged(); }
        }

        private HObject maskObject;

        public HObject MaskObject
        {
            get { return maskObject; }
            set { maskObject = value; RaisePropertyChanged(); }
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

        public void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Shape Model (*.shm)|*.shm"; // 文件格式过滤
            saveFileDialog.Title = "保存Shape模板";

            if (saveFileDialog.ShowDialog() == true) // 用户选择了文件路径
            {
                OperateResult = Service.SaveShapeTemplate(Service.ModelID, saveFileDialog.FileName);
            }
        }


      

        /// <summary>
        /// 执行
        /// </summary>
        private void Run()
        {
            var obj = DrawObjectList.FirstOrDefault();
            if (obj != null)
            {
                if (MaskObject != null)
                {
                    HOperatorSet.Difference(obj.HObject, MaskObject, out HObject regionDiffererce);
                    obj.HObject = regionDiffererce;
                }
                OperateResult = Service.Run(image, obj.HObject);

            }
        }


    }
}
