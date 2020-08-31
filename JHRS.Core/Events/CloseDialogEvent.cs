using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Events
{
    public class CloseDialogEvent : PubSubEvent<DialogParameters>
    {
        public new ICollection<IEventSubscription> Subscriptions => base.Subscriptions;
    }
}
