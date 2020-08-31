using JHRS.Core.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.ViewModels
{
	public abstract class BaseDialogPageViewModel : BaseViewModel
	{
		public BaseDialogPageViewModel(IContainerExtension container)
			: base(container)
		{
			SubscribeEvents();
		}

		protected abstract Task SaveCommand();

		private void SubscribeEvents()
		{
			CommonSaveEvent saveEvent = base.EventAggregator.GetEvent<CommonSaveEvent>();
			saveEvent.Subscriptions.Clear();
			saveEvent.Subscribe(delegate
			{
				SaveCommand();
			});
		}
	}
}
