using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.SystemManage
{
	[Module(ModuleName = "SystemManageModule", OnDemand = true)]
	public class SystemManageModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
		}
	}
}
