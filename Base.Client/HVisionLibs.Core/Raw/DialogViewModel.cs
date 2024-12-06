using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core
{
    /// <summary>
    /// 弹窗基类
    /// </summary>
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "My Dialog";

        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }

        public void CloseDialog()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }
    }
}
