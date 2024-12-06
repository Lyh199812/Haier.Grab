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
    public class TrayViewModel: PageViewModelBase
    {
        private readonly TrayService trayService;
        public TrayViewModel(TrayService trayService,IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator eventAggregator) : base(unityContainer, regionManager)
        {
            this.PageTitle = "托盘状态";
            this.IsCanClose = true;

            this.trayService = trayService;

            LoadCommand = new DelegateCommand(ExecuteLoadCommand);
            SearchCommand = new DelegateCommand(ExecuteSearchCommand);
            GenerateBatchCommand = new DelegateCommand(ExecuteGenerateBatchCommand);
        }
        // 托盘列表
        private ObservableCollection<TTray> trays;
        public ObservableCollection<TTray> Trays
        {
            get => trays;
            set => SetProperty(ref trays, value);
        }

        // 查询结果
        private string searchResult;
        public string SearchResult
        {
            get => searchResult;
            set => SetProperty(ref searchResult,DateTime.Now.ToString()+"_"+ value);
        }

        // 当前输入的托盘ID
        private string trayIDInput;
        public string TrayIDInput
        {
            get => trayIDInput;
            set => SetProperty(ref trayIDInput, value);
        }

        // 加载所有托盘信息的命令
        public ICommand LoadCommand { get; }
        private void ExecuteLoadCommand()
        {
            var result = trayService.LoadAll();
            if (result.IsSuccess)
            {
                Trays = new ObservableCollection<TTray>(result.Content);
                SearchResult = "加载所有托盘信息成功";
            }
            else
            {
                SearchResult = $"加载失败：{result.Message}";
            }
        }

        // 查找托盘信息的命令
        public ICommand SearchCommand { get; }
        private void ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(TrayIDInput))
            {
                SearchResult = "托盘ID不能为空";
                return;
            }

            var result = trayService.Find(TrayIDInput);
            if (result.IsSuccess)
            {
                Trays = new ObservableCollection<TTray> { result.Content };
                SearchResult = $"托盘信息加载成功：{result.Content.TrayID}";
            }
            else
            {
                SearchResult = $"查询失败：{result.Message}";
            }
        }

        // 生成批次号的命令
        public ICommand GenerateBatchCommand { get; }
        private void ExecuteGenerateBatchCommand()
        {
            if (string.IsNullOrWhiteSpace(TrayIDInput))
            {
                SearchResult = "托盘ID不能为空";
                return;
            }

            var result = trayService.GetBatchID(TrayIDInput);
            if (result.IsSuccess)
            {
                SearchResult = $"批次号生成成功：{result.Content}";
            }
            else
            {
                SearchResult = $"生成批次号失败：{result.Message}";
            }
        }
    }
}
