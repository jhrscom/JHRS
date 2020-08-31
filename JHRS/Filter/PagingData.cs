using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Filter
{
    /// <summary>
    /// 分页对象封装
    /// </summary>
    public class PagingData : BindableBase
    {
        /// <summary>
        /// 每页显示数据
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        private int total;

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int Total
        {
            get { return total; }
            set
            {
                total = value;
                MaxPageCount = PageSize == 0 ? 1 : (int)Math.Ceiling((decimal)Total / PageSize);
            }
        }

        private int maxPageCount;
        /// <summary>
        /// 总分页数量
        /// </summary>
        public int MaxPageCount
        {
            get { return maxPageCount; }
            set { SetProperty(ref maxPageCount, value); }
        }
    }
}
