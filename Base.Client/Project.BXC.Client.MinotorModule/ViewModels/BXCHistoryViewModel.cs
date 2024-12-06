using BXC.Client.MinotorModule.BLL;
using BXC.Client.MinotorModule.DAL;
using BXC.Client.MinotorModule.Models;
using Prism.Navigation.Regions;
using Base.Client.BLL;
using Base.Client.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Base.Client.Entity.BXC;
using BXC.Client.DAL;

namespace BXC.Client.MinotorModule.ViewModels
{
    public class BXCHistoryViewModel : BindableBase
    {
        private string _pageTitle = "历史数据";
        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }
        public bool IsCanClose { get; set; } = true;
        public DelegateCommand CloseCommand {  get; set; }

        IUnityContainer _unityContainer;
        IDialogService _dialogService;
        public BXCHistoryViewModel(
            IUnityContainer unityContainer, IFileBLL fileBLL,
            IDialogService dialogService, IRegionManager regionManager)
        {

            _unityContainer = unityContainer;

            _dialogService = dialogService;

            CloseCommand = new DelegateCommand(() =>
            {
                // 区域 对应的一个View列表
                var obj = unityContainer.Registrations.Where(v => v.Name == "BXCHistoryView").FirstOrDefault();
                string name = obj.MappedToType.Name;


                if (!string.IsNullOrEmpty(name))
                {
                    var region = regionManager.Regions["MainViewRegion"];
                    var view = region.Views.Where(v => v.GetType().Name == name).FirstOrDefault();
                    if (view != null)
                        region.Remove(view);
                }
            });


            SearchCommand = new DelegateCommand(ExecuteSearch);
            SaveCommand = new DelegateCommand(SaveDatas);
            ClearTableCommand = new DelegateCommand(ClearTable);

        }

        private void ClearTable()
        {
            if(MessageBox.Show("是否确认清空表格！","警告",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                var rst=DBDAL.ClearBXCRecordEntities();
                {
                    if(rst.IsSuccess) 
                    {
                        MessageBox.Show("清除完成");
                    }
                    else
                    {

                        MessageBox.Show("清除失败"+ rst.Message);

                    }
                }

            }
        }

        private void SaveDatas()
        {
            ExcelHelper.ExportToExcel(FilteredRecords);
            MessageBox.Show("保存完成");

        }

        private List<BXCRecordEntity> _allRecords; // 存储所有的记录

        public DelegateCommand ClearTableCommand {  get; set; }


        public List<BXCRecordEntity> FilteredRecords { get; set; } // 存储过滤后的记录

        private void ExecuteSearch()
        {
            DateTime? startDateTime = StartDate?.Add(StartTime ?? TimeSpan.Zero);
            DateTime? endDateTime = EndDate?.Add(EndTime ?? TimeSpan.Zero);
            var rst = DBDAL.LoadBXCRecordEntitys(startDateTime, endDateTime);
            if (rst.IsSuccess)
            {
                _allRecords = rst.Content;
            }
            else
            {
                MessageBox.Show($"加载历史数据失败+{rst.Message}");
            }
            FilteredRecords = _allRecords?
                .Where(record =>
                    (!startDateTime.HasValue || record.Time >= startDateTime) &&
                    (!endDateTime.HasValue || record.Time <= endDateTime) &&
                    (string.IsNullOrEmpty(SN) || record.SN.Contains(SN)))
                .ToList();

            // RaisePropertyChanged 用于更新 UI
            RaisePropertyChanged(nameof(FilteredRecords));
        }

        private DateTime? _startDate;
        private TimeSpan? _startTime;
        private DateTime? _endDate;
        private TimeSpan? _endTime;
        private string _sn;

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public TimeSpan? StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public TimeSpan? EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        public string SN
        {
            get => _sn;
            set => SetProperty(ref _sn, value);
        }

        public DelegateCommand SearchCommand { get; }

        public DelegateCommand SaveCommand { get; set; }
    }
}
