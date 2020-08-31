using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Shell.ViewModels.Dialogs
{
    /// <summary>
    /// 确认框
    /// </summary>
    public class ConfirmDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;

        private string _title = "未設置標題";

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                SetProperty(ref _message, value, "Message");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value, "Title");
            }
        }

        public DelegateCommand<string> CloseDialogCommand => new DelegateCommand<string>((parameter) =>
        {
            ButtonResult result = ButtonResult.None;
            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.Yes;
            }
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.No;
            }
            RaiseRequestClose(new DialogResult(result));
        });

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            this.RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }

}
