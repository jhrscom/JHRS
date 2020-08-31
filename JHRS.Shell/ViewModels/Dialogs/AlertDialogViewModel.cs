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
	/// 弹框模型
	/// </summary>
	public class AlertDialogViewModel : BindableBase, IDialogAware
	{

		public event Action<IDialogResult> RequestClose;

		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		private string _title = "未設置標題";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public DelegateCommand<string> CloseDialogCommand => new DelegateCommand<string>((parameter)=> {
			ButtonResult result = ButtonResult.None;

			if (parameter?.ToLower() == "true")
				result = ButtonResult.Yes;
			else if (parameter?.ToLower() == "false")
				result = ButtonResult.No;

			RaiseRequestClose(new DialogResult(result));
		});
			
		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
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