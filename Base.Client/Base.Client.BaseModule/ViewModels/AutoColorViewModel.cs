using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AutoColorViewModel : PageViewModelBase
    {
        public ObservableCollection<ColorModel> AutoColors { get; set; }
            = new ObservableCollection<ColorModel>();

        IBaseBLL _baseBLL;
        IDialogService _dialogService;
        Dispatcher _dispatcher;

        public AutoColorViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IBaseBLL baseBLL, IDialogService dialogService)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "车辆颜色信息维护";

            _baseBLL = baseBLL;
            _dialogService = dialogService;
            _dispatcher = unityContainer.Resolve<Dispatcher>();

            Refresh();
        }

        public override void Refresh()
        {
            AutoColors.Clear();
            Task.Run(async () =>
            {
                var acs = await _baseBLL.GetAutoColors();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    _dispatcher.Invoke(() =>
                    {
                        AutoColors.Add(new ColorModel
                        {
                            Index = i + 1,
                            ColorId = item.colorId,
                            ColorName = item.colorName,

                            DeleteCommand = new DelegateCommand<object>(DeleteItem)
                        });
                    });
                }
            });
        }
        // 只有添加   没有编辑
        public override void Add()
        {
            // 打开新增子窗口
            _dialogService.ShowDialog("AddAutoColorDialog", null, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        private async void DeleteItem(object obj)
        {
            // 所有的删除动作，都需要提示，防止误操作
            if (System.Windows.MessageBox.Show("是否确定删除此车辆颜色信息？", "提示",
                System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question)
                == System.Windows.MessageBoxResult.Yes)
            {
                // 逻辑删除

                this.Refresh();
            }
        }
    }
}
