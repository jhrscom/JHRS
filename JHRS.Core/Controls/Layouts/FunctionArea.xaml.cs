using CommonServiceLocator;
using JHRS.Constants;
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

namespace JHRS.Core.Controls.Layouts
{
    /// <summary>
    /// FunctionArea.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionArea : UserControl
    {
        public FunctionArea()
        {
            InitializeComponent();

            RegionManager.SetRegionName(queryRegin, RegionNames.QueryRegin);
            var manager = ServiceLocator.Current.GetInstance<IRegionManager>();
            manager.Regions.Remove(RegionNames.QueryRegin);
            RegionManager.SetRegionManager(queryRegin, manager);
        }

        /// <summary>
        /// 新建按鈕文本
        /// </summary>
        public string AddButtonText { get; set; } = "新建";

        /// <summary>
        /// 是否啟用右側功能按鈕
        /// </summary>
        public bool EnableRightButton { get; set; } = true;

        /// <summary>
        /// 是否啟用搜索按鈕
        /// </summary>
        public bool EnableSerachButton { get; set; } = true;

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnNew.Content = AddButtonText;

            if (EnableRightButton == false) functionArea.Visibility = Visibility.Hidden;
            if (EnableSerachButton == false) btnQuery.Visibility = Visibility.Hidden;
        }
    }
}
