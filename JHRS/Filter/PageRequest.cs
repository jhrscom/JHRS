using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Filter
{
    /// <summary>
    /// 分布查询请求
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 初始化一个<see cref="PageRequest"/>类型的新实例
        /// </summary>
        public PageRequest()
        {
            PageCondition = new PageCondition(1, 20);
            FilterGroup = new FilterGroup();
        }

        /// <summary>
        /// 获取或设置 分页条件信息
        /// </summary>
        public PageCondition PageCondition { get; set; }

        /// <summary>
        /// 获取或设置 查询条件组
        /// </summary>
        public FilterGroup FilterGroup { get; set; }

        /// <summary>
        /// 添加默认排序条件，只有排序为空时有效
        /// </summary>
        public void AddDefaultSortCondition(params SortCondition[] sortConditions)
        {
            if (sortConditions == null) throw new ArgumentNullException($"{nameof(sortConditions)}参数不能为null");
            if (PageCondition.SortConditions.Length == 0)
            {
                PageCondition.SortConditions = sortConditions;
            }
        }
    }
}
