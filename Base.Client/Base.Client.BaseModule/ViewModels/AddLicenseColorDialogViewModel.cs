using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Base.Client.BaseModule.Models;
using Base.Client.Common;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddLicenseColorDialogViewModel : IDialogAware
    {
        public string Title => "添加车牌颜色信息";

        public event Action<IDialogResult> RequestClose2;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }



        public ColorModel LicenseColor { get; set; } = new ColorModel();
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public DialogCloseListener RequestClose { get; }

        public AddLicenseColorDialogViewModel(IBaseBLL baseBLL)
        {
            ConfirmCommand = new DelegateCommand(async () =>
            {
                // 数据往数据库存
                try
                {
                    var result = await baseBLL.SaveLicenseColor(new Entity.ColorEntity
                    {
                        colorName = LicenseColor.ColorName,
                    });
                    // 关闭弹窗
                    DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                    RequestClose.Invoke(dialogResult);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            });

            CancelCommand = new DelegateCommand(() =>
            {
                // 关闭弹窗
                RequestClose.Invoke(new DialogResult());
            });
        }
    }
}
