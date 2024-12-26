using Base.Client.Common;
using Base.Client.IBLL;
using HVisionLibs.Core.TemplateMatch;
using MvCamCtrl.NET;
using Project.Modules.GrabLocate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Modules.GrabLocate.ViewModels
{
    public class ProductionConfigViewModel : PageViewModelBase
    {
        private readonly ProductConfigService _productConfigService;
        private string _productConfigIDInput;
        private string _newProductConfigName;
        private string _searchResult;

        public ProductionConfigViewModel(ProductConfigService productConfigService, IUnityContainer unityContainer, IRegionManager regionManager)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "抓取配方";
            this.IsCanClose = true;
            _productConfigService = productConfigService;

            // 初始化命令
            EnableCommand = new DelegateCommand(EnableProductConfig);
            DisableCommand = new DelegateCommand(DisableProductConfig);
            SelectCommand = new DelegateCommand(SelectProductConfig);
            CreateCommand = new DelegateCommand(CreateProductConfig);
            RefreshCommand = new DelegateCommand(RefreshProductConfigs);
            DeleteCommand= new DelegateCommand(DeleteProductConfig);
        }

        // 输入框 ProductConfig ID 输入的绑定
        public string ProductConfigIDInput
        {
            get => _productConfigIDInput;
            set => SetProperty(ref _productConfigIDInput, value);
        }


        private T_ProductConfig _CurProductConfig;
        public T_ProductConfig CurProductConfig
        {
            get { return _CurProductConfig; }
            set 
            {
                _CurProductConfig = value;
                if(value != null)
                {
                    ProductConfigIDInput= value.Id.ToString();
                }
                RaisePropertyChanged(); 
            }
        }


        // 新建 ProductConfig Name 输入框的绑定
        public string NewProductConfigName
        {
            get => _newProductConfigName;
            set => SetProperty(ref _newProductConfigName, value);
        }

        // 显示搜索结果的文本
        public string SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }


        // 产品配置列表
        public ObservableCollection<T_ProductConfig> ProductConfigs { get; set; } = new ObservableCollection<T_ProductConfig>();


        // 启用配方
        public DelegateCommand EnableCommand { get; }
        private  void EnableProductConfig()
        {
            if (!string.IsNullOrEmpty(ProductConfigIDInput))
            {
                var selectedConfig = ProductConfigs.Where(x => x.Id.ToString() == ProductConfigIDInput).FirstOrDefault();

                if (selectedConfig != null)
                {
                    selectedConfig.IsActive = true;
                    var rst = _productConfigService.UpdateProductConfig(selectedConfig);
                    RefreshProductConfigs();
                    SearchResult = $"产品配置 '{selectedConfig.Name}' 已启用";

                }
                else
                {
                    SearchResult = "未找到匹配的产品配置";
                }
            }
            else
            {
                SearchResult = "请输入有效的配方 ID";
            }
        }

        // 停用配方
        public DelegateCommand DisableCommand { get; }
        private  void DisableProductConfig()
        {
            if (!string.IsNullOrEmpty(ProductConfigIDInput))
            {
                var selectedConfig = ProductConfigs.Where(x => x.Id.ToString() == ProductConfigIDInput).FirstOrDefault();

                if (selectedConfig != null)
                {
                    if (selectedConfig.IsSelected)
                    {
                        SearchResult = "不能停用选中的配方";
                        return;
                    }

                    selectedConfig.IsActive = false;
                    var rst=_productConfigService.UpdateProductConfig(selectedConfig);
                    RefreshProductConfigs();
                    SearchResult = $"产品配置 '{selectedConfig.Name}' 已停用";
               
                }
                else
                {
                    SearchResult = "未找到匹配的产品配置";
                }
            }
            else
            {
                SearchResult = "请输入有效的配方 ID";
            }
        }

        // 选中配方
        public DelegateCommand SelectCommand { get; }
        private void SelectProductConfig()
        {
            if (!string.IsNullOrEmpty(ProductConfigIDInput))
            {
                var rst1 = _productConfigService.FindProductConfig(int.Parse(ProductConfigIDInput));
                var rst2 = _productConfigService.FindProductConfig(ProductConfigs.Where(x=>x.IsSelected).First().Id);
                if (rst1.IsSuccess && rst1.Content != null)
                {
                    var selectedConfig = rst1.Content;
                    // 只有激活的配方才可以被选中
                    if (!selectedConfig.IsActive)
                    {
                        SearchResult = "只能选择已激活的配方";
                        return;
                    }

                    // 设置选中状态，且更新数据库
                    if (rst2.IsSuccess)
                    {
                        var selectedConfig_Current = rst2.Content;

                        // 先取消之前选中的配方的选中状态
                        selectedConfig_Current.IsSelected = false;
                        // 分离旧实体，避免 EF Core 跟踪多个相同主键的实例
                        var rst3 =_productConfigService.UpdateProductConfig(selectedConfig_Current);

                        selectedConfig.IsSelected = true;
                         rst3 = _productConfigService.UpdateProductConfig(selectedConfig);
                        SearchResult = $"产品配置 '{selectedConfig.Name}' 已选中";
                    }
                    else
                    {
                        SearchResult = $"未搜索到选中产品配置"+ rst2.Message;

                    }

                }
                else
                {
                    SearchResult = "未找到匹配的产品配置";
                }
            }
            else
            {
                SearchResult = "请输入有效的配方 ID";
            }
            RefreshProductConfigs();
        }

        //删除配方
        public DelegateCommand DeleteCommand { get; }
        private void DeleteProductConfig()
        {
            if (!string.IsNullOrEmpty(ProductConfigIDInput))
            {
                var rst1 = _productConfigService.FindProductConfig(int.Parse(ProductConfigIDInput));
                if (rst1.IsSuccess && rst1.Content != null)
                {
                    var selectedConfig = rst1.Content;
                    // 只有未选中的配方可删除
                    if (selectedConfig.IsSelected)
                    {
                        SearchResult = "不能删除被选中的配方";
                        return;
                    }
                    if(MessageBox.Show($"是否确认删除配方 ID【{ProductConfigIDInput}】 名称:{selectedConfig.Name}" +
                        $"\r\n关联型号：{selectedConfig.AssociatedModel}","删除警告",MessageBoxButton.OKCancel)!=MessageBoxResult.OK)
                    {
                        return;
                    }
                    var result = _productConfigService.DeleteProductConfig(int.Parse (ProductConfigIDInput));
                    SearchResult = result.Message;
                    RefreshProductConfigs();

                }
                else
                {
                    SearchResult = "未找到匹配的产品配置";
                }
            }
            else
            {
                SearchResult = "请输入有效的配方 ID";
            }
        }

        // 新建命令
        public DelegateCommand CreateCommand { get; }
        private  void CreateProductConfig()
        {
            if (!string.IsNullOrWhiteSpace(NewProductConfigName))
            {
                if (ProductConfigs.Any(x => x.Name == NewProductConfigName))
                {
                    SearchResult = $"配方名称 '{NewProductConfigName}' 已存在，请选择一个新的名称";
                    return;
                }
                var SelectedProductConfig = ProductConfigs.Where(x => x.IsSelected).First();
                if (SelectedProductConfig != null && SelectedProductConfig.IsActive && SelectedProductConfig.IsSelected)
                {
                    var newConfig = new T_ProductConfig
                    {
                        Name = NewProductConfigName,
                        ModelPath = SelectedProductConfig.ModelPath,
                        MinScore = SelectedProductConfig.MinScore,
                        MinScoreForCheck = SelectedProductConfig.MinScoreForCheck,
                        TargetCount = SelectedProductConfig.TargetCount,

                        IsActive = true,
                        IsSelected = false
                    };

                    var result = _productConfigService.AddProductConfig(newConfig);
                    SearchResult = result.IsSuccess ? $"新建产品配置 '{newConfig.Name}' 成功" : $"新建失败: {result.Message}";

                    if (result.IsSuccess)
                    {
                        ProductConfigs.Add(newConfig);
                    }
                }
                else
                {
                    SearchResult = "请选择一个激活且选中的配方作为参考";
                }
            }
            else
            {
                SearchResult = "请提供配方名称";
            }
        }

        // 刷新命令
        public DelegateCommand RefreshCommand { get; }
        private  void RefreshProductConfigs()
        {
            var result =  _productConfigService.GetAllProductConfigs();
            if (result.IsSuccess)
            {
                ProductConfigs.Clear();

                foreach (var config in result.Content)
                {
                    ProductConfigs.Add(config);
                }
                SearchResult = "刷新成功";
            }
            else
            {
                SearchResult = $"刷新失败: {result.Message}";
            }
        }
    }

}
