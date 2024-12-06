using Base.Client.Common;
using Project.IMU.DataHub.BLL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.IMU.DataHub.ViewModels
{
    public class BatchViewModel : PageViewModelBase
    {
        private readonly BatchService _batchService;

        // 用于存储原始的批次数据
        private ObservableCollection<TBatch> _allBatches;
        public ObservableCollection<TBatch> AllBatches
        {
            get => _allBatches;
            set => SetProperty(ref _allBatches, value);
        }

        // 用于界面展示的筛选后数据
        private ObservableCollection<TBatch> _trays;
        public ObservableCollection<TBatch> Trays
        {
            get => _trays;
            set => SetProperty(ref _trays, value);
        }

        public BatchViewModel(BatchService batchService, IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator eventAggregator)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "批次查询";
            this.IsCanClose = true;
            this._batchService = batchService;

            // 初始化命令
            SearchCommand = new DelegateCommand(SearchBatches);
            LoadCommand = new DelegateCommand(LoadBatches);
        }

        // 属性绑定
        private string _currentStationInput;
        public string CurrentStationInput
        {
            get => _currentStationInput;
            set => SetProperty(ref _currentStationInput, value);
        }

        private string _trayIDInput;
        public string TrayIDInput
        {
            get => _trayIDInput;
            set => SetProperty(ref _trayIDInput, value);
        }

        private string _batchIDInput;
        public string BatchIDInput
        {
            get => _batchIDInput;
            set => SetProperty(ref _batchIDInput, value);
        }

        private string _statusInput;
        public string StatusInput
        {
            get => _statusInput;
            set => SetProperty(ref _statusInput, value);
        }

        private string searchResult;
        public string SearchResult
        {
            get => searchResult;
            set => SetProperty(ref searchResult, DateTime.Now.ToString() + "_" + value);
        }

        // 命令
        public ICommand SearchCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        // 搜索批次
        private void SearchBatches()
        {
            // 如果 AllBatches 为空，先刷新批次数据
            if (AllBatches == null || !AllBatches.Any())
            {
                LoadBatches(); // 刷新数据
            }

            // 使用 LINQ 进行搜索
            var query = AllBatches.AsQueryable();

            // 根据输入条件进行动态过滤
            if (!string.IsNullOrEmpty(BatchIDInput))
            {
                query = query.Where(t => t.BatchID.Contains(BatchIDInput));
            }

            if (!string.IsNullOrEmpty(TrayIDInput))
            {
                query = query.Where(t => t.TrayID.Contains(TrayIDInput));
            }

            if (!string.IsNullOrEmpty(StatusInput))
            {
                query = query.Where(t => t.Status.Contains(StatusInput));
            }

            if (!string.IsNullOrEmpty(CurrentStationInput))
            {
                query = query.Where(t => t.CurrentStation.Contains(CurrentStationInput));
            }

            // 执行查询并更新 Trays
            var filteredResults = query.ToList();

            // 如果有搜索结果，更新 Trays；否则提示无数据
            if (filteredResults.Any())
            {
                Trays = new ObservableCollection<TBatch>(filteredResults);
                SearchResult = "查询成功";
            }
            else
            {
                SearchResult = "没有符合条件的批次";
            }
        }

        // 刷新批次数据
        private void LoadBatches()
        {
            var result = _batchService.LoadAll();
            if (result.IsSuccess)
            {
                // 保存原始数据
                AllBatches = new ObservableCollection<TBatch>(result.Content);
                // 默认显示所有批次数据
                Trays = AllBatches;
                SearchResult = "刷新成功";
            }
            else
            {
                SearchResult = result.Message;
            }
        }
    }

}
