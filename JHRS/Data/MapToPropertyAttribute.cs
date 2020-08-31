using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Data
{
    /// <summary>
    /// 映射到具体属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapToPropertyAttribute : Attribute
    {
        /// <summary>
        /// 映射到字典数据
        /// </summary>
        /// <param name="mapTo"></param>
        public MapToPropertyAttribute(string mapTo)
        {
            this.MapTo = mapTo;
        }

        /// <summary>
        /// 映射字段
        /// </summary>
        public string MapTo { get; set; }
    }
}
