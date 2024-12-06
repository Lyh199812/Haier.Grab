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
    public class LicenseColorViewModel : PageViewModelBase
    {
        public ObservableCollection<ColorModel> LicenseColors { get; set; } = new ObservableCollection<ColorModel>();

        IBaseBLL _baseBLL;
        IDialogService _dialogService;
        Dispatcher _dispatcher;

        public LicenseColorViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IBaseBLL baseBLL, IDialogService dialogService)
            : base(unityContainer, regionManager)
        {
            this.PageTitle = "车牌颜色信息维护";

            _baseBLL = baseBLL;
            _dialogService = dialogService;
            _dispatcher = unityContainer.Resolve<Dispatcher>();

            Refresh();
        }

        public override void Refresh()
        {
            LicenseColors.Clear();
            Task.Run(async () =>
            {
                var acs = await _baseBLL.GetAutoColors();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    _dispatcher.Invoke(() =>
                    {
                        LicenseColors.Add(new ColorModel
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

        public override void Add()
        {
            // 打开新增子窗口
            _dialogService.ShowDialog("AddLicenseColorDialog", null, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        private async void DeleteItem(object obj)
        {
            // 所有的删除动作，都需要提示，防止误操作
            if (System.Windows.MessageBox.Show("是否确定删除此车牌颜色信息？", "提示",
                System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question)
                == System.Windows.MessageBoxResult.Yes)
            {
                // 
                //var model = obj as UserInfoModel;
                //if (model != null)
                //    await _userBLL.ChangeState(model.UserId, 0);// 逻辑删除

                this.Refresh();
            }
        }
    }
}
