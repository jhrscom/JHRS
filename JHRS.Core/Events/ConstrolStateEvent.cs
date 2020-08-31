using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Events
{
    public class ConstrolStateEvent : PubSubEvent<ControlState>
    {
        public new ICollection<IEventSubscription> Subscriptions => base.Subscriptions;
    }
}
