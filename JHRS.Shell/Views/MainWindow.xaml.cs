using CommonServiceLocator;
using JHRS.Core.Events;
using JHRS.Core.Modules;
using Prism.Events;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JHRS.Shell.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PageEvent pageEvent = ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<PageEvent>();
            pageEvent.Subscribe((p) =>
            {
                MenuEntity menu = p.Menu;
                AddPage(menu.Name, p.Page);
            });
        }

        private void AddPage(string name, Page page)
        {
            TabItem tabItem = MainTabPanel.Items.OfType<TabItem>().FirstOrDefault(item => item.Header.ToString() == name);
            if (tabItem == null)
            {
                tabItem = new TabItem()
                {
                    Header = name,
                };
                var pageFrame = new Frame();
                pageFrame.Focusable = false;
                pageFrame.BorderThickness = new Thickness(0);
                pageFrame.Margin = new Thickness(20);
                pageFrame.Navigate(page);
                tabItem.Content = pageFrame;
                MainTabPanel.Items.Add(tabItem);
            }
            MainTabPanel.SelectedItem = tabItem;
        }
    }

    public static class ControlHelper
    {
        public static T FindVisualParent<T>(DependencyObject sender) where T : DependencyObject
        {
            do
            {
                sender = VisualTreeHelper.GetParent(sender);
            }
            while (sender != null && !(sender is T));
            return sender as T;
        }
    }

    /// <summary>
    /// 主菜单样式选择器
    /// </summary>
    public class MenuStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            MenuEntity functionItem = item as MenuEntity;
            if (functionItem.IsGroup)
                return ControlHelper.FindVisualParent<Menu>(container).Resources["MainMenuStyle"] as Style;
            else
                return null;
        }
    }
}