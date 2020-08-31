using JHRS.Constants;
using JHRS.Core.Events;
using JHRS.Core.ViewModels;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHRS.Shell.ViewModels.Dialogs
{
	public class CommonDialogPageViewModel : BaseViewModel, IDialogAware
	{
		/// <summary>
		/// 主界面构造函数
		/// </summary>
		/// <param name="container"></param>
		public CommonDialogPageViewModel(IContainerExtension container) : base(container)
		{
			var dialogEvent = EventAggregator.GetEvent<CloseDialogEvent>();
			dialogEvent.Subscriptions.Clear();
			dialogEvent.Subscribe((p) =>
			{
				ButtonResult result = ButtonResult.Yes;
				RaiseRequestClose(new DialogResult(result, p));
			});

		}

		private string _page;
		public string Page
		{
			get { return _page; }
			set { SetProperty(ref _page, value); }
		}

		private string _title = "未设置标题";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private UIElement _windowIcon;

		public UIElement WindowIcon
		{
			get { return _windowIcon; }
			set { SetProperty(ref _windowIcon, value); }
		}

		/// <summary>
		/// 保存事件
		/// </summary>
		public DelegateCommand SaveCommand => new DelegateCommand(() =>
		{
			EventAggregator.GetEvent<CommonSaveEvent>().Publish();
			EventAggregator.GetEvent<ConstrolStateEvent>().Publish(new ControlState { IsEnabled = false });
		});

		/// <summary>
		/// 关闭对话框命令
		/// </summary>
		public DelegateCommand<string> CloseDialogCommand => new DelegateCommand<string>((parameter) =>
		{
			ButtonResult result = ButtonResult.None;

			if (parameter?.ToLower() == "true")
				result = ButtonResult.Yes;
			else if (parameter?.ToLower() == "false")
				result = ButtonResult.No;

			var r = new DialogResult(result);
			RaiseRequestClose(r);
		});

		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}

		/// <summary>
		/// 能否关闭弹框 
		/// </summary>
		/// <returns></returns>
		public bool CanCloseDialog()
		{
			return true;
		}

		/// <summary>
		/// 弹框关闭后触发
		/// </summary>
		public void OnDialogClosed()
		{

		}

		public event Action<IDialogResult> RequestClose;

		/// <summary>
		/// 弹框打开后触发
		/// </summary>
		/// <param name="parameters"></param>
		public void OnDialogOpened(IDialogParameters parameters)
		{
			Page = parameters.GetValue<string>("page");
			if (!string.IsNullOrWhiteSpace(Page))
			{
				var args = parameters.GetValue<object>("args");
				if (args != null) RegionManager.Regions[RegionNames.DialogRegin].Context = args;
				Navigate(RegionNames.DialogRegin, Page);
			}
			var icon = parameters.GetValue<string>("icon");
			if (!string.IsNullOrEmpty(icon))
			{
				WindowIcon = Application.Current.FindResource(icon) as UIElement;
			}
			if (parameters.GetValue<bool>("disableArea"))
			{
				EventAggregator.GetEvent<DisableDialogPageButtonEvent>().Publish();
			}
			
			Title = parameters.GetValue<string>("title");
		}
	}
}