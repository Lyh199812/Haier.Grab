using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.Client.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose2;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title").ToString();
            Message = parameters.GetValue<string>("msg").ToString();
        }


        private string _msg;
        public string Message
        {
            get { return _msg; }
            set { SetProperty(ref _msg, value); }
        }


        public ICommand ConfirmCommand { get; set; }

        public DialogCloseListener RequestClose { get; }

       
        public MessageViewModel()
        {
            ConfirmCommand = new DelegateCommand(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            });
        }
    }
}
