using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace Base.Client.Common
{
    public abstract class PageViewModelBase : BindableBase, INavigationAware
    {
        #region 自定义基本属性
        // 页面标题
        public string PageTitle { get; set; } = "页面标题";
        // 是否允许关闭
        public bool IsCanClose { get; set; } = true;
        // 新增按钮的文本
        public string AddButtonText { get; set; } = "新增";

        // 搜索关键字
        private string _keyword = "";
        public string Keyword
        {
            get { return _keyword; }
            set { SetProperty(ref _keyword, value); }
        }

        // 关闭Tab页
        public ICommand CloseCommand { get; set; }
        // 刷新当前页面内容
        public ICommand RefreshCommand { get; set; }
        // 新增
        public ICommand AddCommand { get; set; }

        private string NavAddress { get; set; }

        #endregion

        #region 自定义通用方法
        public virtual void Refresh() { }
        public virtual void Add() { }

        // 检索 
        public void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Refresh();
            }
        }
        #endregion

        #region 实现INavigationAware接口方法
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.NavAddress = navigationContext.Uri.ToString();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        public PageViewModelBase(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            RefreshCommand = new DelegateCommand(Refresh);
            AddCommand = new DelegateCommand(Add);

            CloseCommand = new DelegateCommand(() =>
             {
                 // 区域 对应的一个View列表     fuv   页面导航路由
                 var obj = unityContainer.Registrations.Where(v => v.Name == NavAddress).FirstOrDefault();
                 string name = obj.MappedToType.Name;


                 if (!string.IsNullOrEmpty(name))
                 {
                     var region = regionManager.Regions["MainViewRegion"];
                     var view = region.Views.Where(v => v.GetType().Name == name).FirstOrDefault();
                     if (view != null)
                         region.Remove(view);
                 }
             });
        }


    }
}
