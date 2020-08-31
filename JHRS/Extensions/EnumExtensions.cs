using JHRS.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Extensions
{
    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.GetDescription() : value.ToString();
        }

        /// <summary>
        /// 获取枚举项上标注的指定特性对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fd = type.GetField(value.ToString());
            if (fd == null) return default;
            return (T)fd.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }

        /// <summary>
        /// 将枚举转换成实体集合
        /// </summary>
        /// <param name="enum">当前枚举值</param>
        /// <returns></returns>
        public static IList<EnumEntity> GetEnumEntities<T>() where T : Enum
        {
            List<EnumEntity> list = new List<EnumEntity>();
            Type t = typeof(T);
            FieldInfo[] fieldinfo = t.GetFields(); //获取字段信息对象集合
            foreach (FieldInfo field in fieldinfo)
            {
                if (!field.IsSpecialName)
                {
                    list.Add(new EnumEntity
                    {
                        Name = field.Name,
                        Value = (int)field.GetRawConstantValue()
                    });
                }
            }
            return list;
        }
    }

    /// <summary>
    /// 枚举临时实体
    /// </summary>
    public class EnumEntity
    {
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }
    }
}
