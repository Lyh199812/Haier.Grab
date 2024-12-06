using Base.Client.Common;
using Project.IMU.DataHub.BLL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.IMU.DataHub.ViewModels
{
    public class TestDataViewModel : PageViewModelBase
    {
        private readonly BatchTestDataService _batchService;
        public TestDataViewModel(BatchTestDataService testDataService, IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator eventAggregator) : base(unityContainer, regionManager)
        {
            this.PageTitle = "测试数据";
            this.IsCanClose = true;

            _batchService = testDataService;
            SearchCommand = new DelegateCommand(OnSearch);
            RefreshCommand = new DelegateCommand(OnRefresh);

        }
        #region 属性绑定

        // 全部测试数据
        private ObservableCollection<BatchTestData> _allData;
        public ObservableCollection<BatchTestData> AllData
        {
            get => _allData;
            set => SetProperty(ref _allData, value);
        }

        // 当前显示的测试数据
        private ObservableCollection<BatchTestData> _testDataList = new ObservableCollection<BatchTestData>();
        public ObservableCollection<BatchTestData> TestDataList
        {
            get => _testDataList;
            set => SetProperty(ref _testDataList, value);
        }

        // 搜索条件
        public string SearchBatchID { get; set; }
        public string SearchStation { get; set; }
        public string SearchPosition { get; set; }
        public string SearchTestItem { get; set; }
        public string SearchTestResult { get; set; }

        public DateTime SearchStartDate { get; set; } = DateTime.Now.AddDays(-1);
        public string SearchStartHour { get; set; }
        public string SearchStartMinute { get; set; }
        public DateTime SearchEndDate { get; set; }=DateTime.Now.AddDays(1);
        public string SearchEndHour { get; set; }
        public string SearchEndMinute { get; set; }

        private string _searchResult;
        public string SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        #endregion

        #region 命令

        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand SearchCommand { get; }

        #endregion

        #region 方法

        private async void OnRefresh()
        {
            // 记录开始时间
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            // 异步加载数据
            await Task.Run(() =>
            {
                var data = _batchService.LoadAll();
                if (data != null && data.Content != null)
                {
                    // 使用 Dispatcher 确保在 UI 线程中更新
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AllData = new ObservableCollection<BatchTestData>(data.Content);
                        TestDataList = new ObservableCollection<BatchTestData>(AllData);
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AllData = new ObservableCollection<BatchTestData>();
                        TestDataList = new ObservableCollection<BatchTestData>();
                    });
                }
            });

            // 停止计时并计算耗时
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // 更新搜索结果
            SearchResult = $"共加载 {AllData.Count} 条数据，用时 {elapsedMilliseconds} 毫秒";
        }


        private async void OnSearch()
        {
            if(AllData==null)
            {
                SearchResult = $"请先刷新数据后进行查询";
                return;
            }
            // 转换时间区间
            var startTime = CombineDateTime(SearchStartDate, SearchStartHour, SearchStartMinute);
            var endTime = CombineDateTime(SearchEndDate, SearchEndHour, SearchEndMinute);

            // 在 AllData 中筛选数据
            var filtered = AllData.Where(data =>
                (string.IsNullOrEmpty(SearchBatchID) || data.BatchID.Contains(SearchBatchID)) &&
                (string.IsNullOrEmpty(SearchStation) || data.Station.Contains(SearchStation)) &&
                (string.IsNullOrEmpty(SearchPosition) || data.PositionIndex.ToString().Contains(SearchPosition)) &&
                (string.IsNullOrEmpty(SearchTestItem) || data.TestItem.Contains(SearchTestItem)) &&
                (string.IsNullOrEmpty(SearchTestResult) || data.TestResult.Contains(SearchTestResult)) &&
                data.TestTime >= startTime &&
                data.TestTime <= endTime);

            // 更新数据列表
            TestDataList = new ObservableCollection<BatchTestData>(filtered);

            // 显示搜索结果
            SearchResult = $"查询到 {TestDataList.Count} 条匹配的数据";
        }

        private DateTime CombineDateTime(DateTime date, string hour, string minute)
        {
            int h = int.TryParse(hour, out var parsedHour) ? parsedHour : 0;
            int m = int.TryParse(minute, out var parsedMinute) ? parsedMinute : 0;
            return date.AddHours(h).AddMinutes(m);
        }

        #endregion
    }
}
