using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Base.Client.Upgrade.ViewModels
{
    public class FinishViewModel : BindableBase
    {
        private bool _isRun;
        public bool IsRun
        {
            get { return _isRun; }
            set { SetProperty(ref _isRun, value); }
        }

        public ICommand ConfirmCommand { get; set; }

        public FinishViewModel()
        {
            ConfirmCommand = new DelegateCommand(() =>
            {
                if (IsRun)
                {
                    Process process = Process.Start("Base.Client.exe");
                    process.WaitForInputIdle();
                }

                Application.Current.Shutdown();
            });
        }
    }
}
