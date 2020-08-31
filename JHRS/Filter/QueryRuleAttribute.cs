using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Filter
{
    /// <summary>
    /// 查询规则特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryRuleAttribute : Attribute
    {
        /// <summary>
        /// 查询字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 查询操作符
        /// </summary>
        public FilterOperate Operate { get; set; }

        /// <summary>
        /// 分组查询操作符（生成sql后面的where 带括号的查询，取值只能为or 或 and）
        /// </summary>
        public FilterOperate GroupOperate { get; set; }

        /// <summary>
        /// 查询规则构造函数
        /// </summary>
        /// <param name="operate">操作符</param>
        /// <param name="fieldName">数据库可接受的查询字段名称，未传直接取属性名称</param>
        /// <param name="group">隶属分组</param>
        /// <param name="groupOperate">分组查询操作符</param>
        public QueryRuleAttribute(FilterOperate operate, string fieldName, string group = "", FilterOperate groupOperate = FilterOperate.And)
        {
            FieldName = fieldName;
            Group = group;
            Operate = operate;
            GroupOperate = groupOperate;
        }
    }
}
