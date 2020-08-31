using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JHRS.Data
{
    /// <summary>
    /// DataGrid绑定数据源描述
    /// </summary>
    public class BindDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string HeaderName { get; set; }

        /// <summary>
        /// 显示为
        /// </summary>
        public ShowScheme ShowAs { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// DataGrid列绑定属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 应用内的容模板Key
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public DataGridLength Width { get; set; }

        /// <summary>
        /// 列宽ByGrid
        /// </summary>
        public GridLength CloumnWidth { get; set; }


        /// <summary>
        /// DataGrid绑定数据源描述
        /// </summary>
        /// <param name="headerName">列名</param>
        /// <param name="showAs">显示为</param>
        /// <param name="width">宽度</param>
        /// <param name="displayIndex">显示顺序</param>
        /// <param name="resourceKey">自定义列Key</param>
        public BindDescriptionAttribute(string headerName, ShowScheme showAs = ShowScheme.普通文本, string width = "Auto", int displayIndex = 0, string resourceKey = "")
        {
            HeaderName = headerName;
            DisplayIndex = displayIndex;
            ResourceKey = resourceKey;
            ShowAs = showAs;
            var convert = new DataGridLengthConverter();
            Width = (DataGridLength)convert.ConvertFrom(width);
            var gridCOnvert = new GridLengthConverter();
            CloumnWidth = (GridLength)gridCOnvert.ConvertFrom(width);

            if (showAs == ShowScheme.自定义 && string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentException($"自定义列时需要指定{nameof(resourceKey)}参数！");
        }
    }

    /// <summary>
    /// 展示方式
    /// </summary>
    public enum ShowScheme
    {
        普通文本 = 1,
        自定义 = 4
    }
}
