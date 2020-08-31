using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Modules
{
    /// <summary>
    /// 查询区域定位对象，用于标记某个控件加载到对应位置
    /// </summary>
	public class QueryLocatorAttribute : Attribute
	{
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 将当前控件加载到指定的Page或Window目标对象
        /// </summary>
        public Type Target { get; set; }

        /// <summary>
        /// 查询区域定位构造函数
        /// </summary>
        /// <param name="regionName">区域名称</param>
        /// <param name="type">目标对象</param>
        public QueryLocatorAttribute(string regionName, Type type)
        {
            RegionName = regionName;
            Target = type;
        }
    }
}
