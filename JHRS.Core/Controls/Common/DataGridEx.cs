using JHRS.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JHRS.Core.Controls.Common
{
    /// <summary>
    /// 輕量級的DataGrid擴展
    /// </summary>
    public class DataGridEx : DataGrid
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public DataGridEx()
        {
            this.AutoGenerateColumns = false;
            this.Loaded += DataGridEx_Loaded;
            this.LoadingRow += PagingDataList_LoadingRow;
        }

        /// <summary>
        /// 给表格添加样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridEx_Loaded(object sender, RoutedEventArgs e)
        {
            this.CanUserAddRows = false;
        }

        /// <summary>
        /// 生成序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagingDataList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (EnableRowNumber)
                //需要分页
                e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 操作列key
        /// </summary>
        public string OperatingKey { get; set; } = string.Empty;
        /// <summary>
        /// 操作列的宽度
        /// </summary>
        public DataGridLength OperationWidth { get; set; }

        /// <summary>
        /// 是否启用序号
        /// </summary>
        public bool EnableRowNumber { get; set; } = true;

        /// <summary>
        /// 禁止显示的列
        /// </summary>
        public string DisableCloumn { get; set; }

        public IEnumerable<object> DataSource
        {
            get { return (IEnumerable<object>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        private bool IsGenerateColumns = false;
        // Using a DependencyProperty as the backing store for DataSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable<object>), typeof(DataGridEx), new PropertyMetadata((d, e) =>
            {
                DataGridEx u = d as DataGridEx;
                u.ItemsSource = u.DataSource;

                if (u.IsGenerateColumns || u.DataSource == null || u.DataSource.Count() == 0) return;
                var index = 0;
                if (u.EnableRowNumber)
                {
                    var acolumn = new DataGridTextColumn
                    {
                        Header = "序号",
                        Width = new DataGridLength(50),
                        Binding = new Binding("Header") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridRow), 1) }
                    };
                    u.Columns.Insert(0, acolumn);
                    index++;
                }
                u.GenerateColumns(index, u.ItemsSource, u.OperatingKey, u.OperationWidth);
                u.IsGenerateColumns = true;

            }));


        //protected override void OnInitialized(EventArgs e)
        //{
        //    if (IsGenerateColumns || ItemsSource == null) return;
        //    var index = 0;
        //    if (EnableRowNumber)
        //    {
        //        var acolumn = new DataGridTextColumn
        //        {
        //            Header = "序号",
        //            Width = new DataGridLength(50),
        //            Binding = new Binding("Header") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridRow), 1) }
        //        };
        //        this.Columns.Insert(0, acolumn);
        //        index++;
        //    }
        //    this.GenerateColumns(index, ItemsSource, OperatingKey, OperationWidth);
        //    IsGenerateColumns = true;
        //}
    }
}