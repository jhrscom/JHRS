using JHRS.Constants;
using JHRS.Core.Modules;
using JHRS.OutpatientSystem.Views;
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

namespace JHRS.OutpatientSystem.Controls
{
    /// <summary>
    /// ReservationQuery.xaml 的交互逻辑
    /// </summary>
    [QueryLocator(RegionNames.QueryRegin, typeof(Reservation))]
    public partial class ReservationQuery : UserControl
    {
        public ReservationQuery()
        {
            InitializeComponent();
        }
    }
}
