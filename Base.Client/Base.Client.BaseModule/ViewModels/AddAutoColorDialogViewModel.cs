using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class AddAutoColorDialogViewModel : IDialogAware
    {
        public string Title => "添加车辆颜色信息";

       // public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }


        public ColorModel AutoColor { get; set; } = new ColorModel();
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public DialogCloseListener RequestClose { get; }

        public AddAutoColorDialogViewModel(IBaseBLL baseBLL)
        {
            ConfirmCommand = new DelegateCommand(async () =>
            {
                // 数据往数据库存
                try
                {
                    var result = await baseBLL.SaveAutoColor(new Entity.ColorEntity
                    {
                        colorName = AutoColor.ColorName,
                    });
                    if (result)
                    {
                        // 关闭弹窗
                        DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                        RequestClose.Invoke(dialogResult);
                    }
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
