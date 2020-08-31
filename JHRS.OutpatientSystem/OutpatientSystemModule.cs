using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.OutpatientSystem
{
    /// <summary>
    /// 預約掛號模塊
    /// </summary>
    [Module(ModuleName = "OutpatientSystemModule", OnDemand = true)]
    public class OutpatientSystemModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
