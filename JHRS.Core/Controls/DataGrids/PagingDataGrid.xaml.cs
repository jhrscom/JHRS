using JHRS.Core.Extensions;
using JHRS.Filter;
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

namespace JHRS.Core.Controls.DataGrids
{
    /// <summary>
    /// PagingDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class PagingDataGrid : UserControl
    {
        public PagingDataGrid()
        {
            InitializeComponent();
            pagingDataList.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 生成序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagingDataList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (EnablePagination)
                //需要分页
                e.Row.Header = (PagingData.PageIndex - 1) * PagingData.PageSize + e.Row.GetIndex() + 1;
            else
                //不需要分页
                e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 表格数据源
        /// </summary>
        public IEnumerable<object> PageData
        {
            get { return (IEnumerable<object>)GetValue(PageDataProperty); }
            set { SetValue(PageDataProperty, value); }
        }

        //是否已经生成了列并建立了绑定关系 
        private bool IsGenerateColumns = false;
        // Using a DependencyProperty as the backing store for PageData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageDataProperty =
            DependencyProperty.Register("PageData", typeof(IEnumerable<object>), typeof(PagingDataGrid), new PropertyMetadata((d, e) =>
            {
                PagingDataGrid pagingDataGrid = d as PagingDataGrid;
                if (pagingDataGrid.IsGenerateColumns) return;
                int num = 0;
                if (pagingDataGrid.EnableRowNumber) num++;

                if (pagingDataGrid.EnableCheckBoxColumn) num++;

                pagingDataGrid.pagingDataList.GenerateColumns(num, e.NewValue, pagingDataGrid.OperatingKey, pagingDataGrid.OperatingWidth);
                pagingDataGrid.IsGenerateColumns = true;
            }));


        /// <summary>
        /// 分页控件数据源
        /// </summary>
        public PagingData PagingData
        {
            get { return (PagingData)GetValue(PagingDataProperty); }
            set { SetValue(PagingDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PagingData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagingDataProperty =
            DependencyProperty.Register("PagingData", typeof(PagingData), typeof(PagingDataGrid));

        /// <summary>
        /// 是否启用分页功能
        /// </summary>
        public bool EnablePagination { get; set; } = true;

        /// <summary>
        /// 是否启用序号
        /// </summary>
        public bool EnableRowNumber { get; set; } = true;

        /// <summary>
        /// 是否复选框列
        /// </summary>
        public bool EnableCheckBoxColumn { get; set; } = false;

        /// <summary>
        /// 复选框列是否启用全选功能
        /// </summary>
        public bool EnableSelectAll { get; set; } = false;

        /// <summary>
        /// 操作列的Key
        /// </summary>
        public string OperatingKey { get; set; }

        /// <summary>
        /// 操作列宽
        /// </summary>
        public DataGridLength OperatingWidth { get; set; }

        /// <summary>
        /// 当前选中数据
        /// </summary>
        public IEnumerable<object> CheckedList
        {
            get { return (IEnumerable<object>)GetValue(SelectedListProperty); }
            set { SetValue(SelectedListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedListProperty =
            DependencyProperty.Register("CheckedList", typeof(IEnumerable<object>), typeof(PagingDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// 初始化表格行为
        /// </summary>
        private void InintBehavior()
        {
            if (!EnablePagination) ucDataGrid.Children.Remove(ucPagination);
            if (!EnableRowNumber) pagingDataList.Columns.Remove(pagingDataList.Columns.FirstOrDefault(x => x.Header.ToString() == "序号"));

            if (!EnableCheckBoxColumn) pagingDataList.Columns.Remove(isCheckbox);
            if (!EnableSelectAll) isCheckbox.Header = "选择";
        }

        /// <summary>
        /// 用户控件初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InintBehavior();
        }

        /// <summary>
        /// 复选框全选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var c = sender as CheckBox;
            CheckedAll(pagingDataList, c.IsChecked);
            CheckedList = GetSelected();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="isChecked"></param>
        private void CheckedAll(DependencyObject parent, bool? isChecked)
        {
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = VisualTreeHelper.GetChild(parent, i);
                CheckBox child = v as CheckBox;

                if (child == null)
                {
                    CheckedAll(v, isChecked);
                }
                else
                {
                    child.IsChecked = isChecked;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取所有选中项
        /// </summary>
        /// <returns></returns>
        private List<object> GetSelected()
        {
            List<object> list = new List<object>();
            foreach (var item in pagingDataList.ItemsSource)
            {
                var m = isCheckbox.GetCellContent(item);
                var c = m.GetChildObject<CheckBox>("chkItem");
                if (c != null && c.IsChecked == true)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 单击选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkItem_Click(object sender, RoutedEventArgs e)
        {
            CheckedList = GetSelected();
        }
    }
}
