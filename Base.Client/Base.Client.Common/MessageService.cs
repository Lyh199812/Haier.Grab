using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Client.Common
{
    public class MessageService
    {
        IDialogService _dialogService;
        public MessageService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public bool Show(string message, string title = "系统提示")
        {
            bool state = false;
            DialogParameters param = new DialogParameters();
            param.Add("title", title);
            param.Add("msg", message);
            _dialogService.ShowDialog(
                "MessageView",
                param,
                result =>
                {
                    state = result.Result == ButtonResult.OK;
                    autoResetEvent.Set();
                }
                );

            autoResetEvent.WaitOne();
            return state;
        }
    }
}
