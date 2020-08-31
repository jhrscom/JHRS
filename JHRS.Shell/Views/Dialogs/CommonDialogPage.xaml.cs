using CommonServiceLocator;
using JHRS.Constants;
using JHRS.Core.Events;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JHRS.Shell.Views.Dialogs
{
    /// <summary>
    /// CommonDialogPage.xaml 的交互逻辑
    /// </summary>
    public partial class CommonDialogPage : Page
    {
        public CommonDialogPage()
        {
            InitializeComponent();

            RegionManager.SetRegionName(pages, RegionNames.DialogRegin);
            IRegionManager manager = ServiceLocator.Current.GetInstance<IRegionManager>();
            manager.Regions.Remove("DialogRegin");
            RegionManager.SetRegionManager(pages, manager);

            ConstrolStateEvent controlEvent = ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ConstrolStateEvent>();
            controlEvent.Subscriptions.Clear();
            controlEvent.Subscribe((state) => { SaveButton.IsEnabled = state.IsEnabled; });

            DisableDialogPageButtonEvent disableEvent = ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<DisableDialogPageButtonEvent>();
            disableEvent.Subscriptions.Clear();
            disableEvent.Subscribe(() => { saveArea.Visibility = Visibility.Collapsed; });
        }
    }
}
