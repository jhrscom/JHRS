using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Modules
{
    /// <summary>
    /// 用户控件管理器
    /// </summary>
    public class UserControlManager
    {
        private static readonly ConcurrentDictionary<string, ControlMapping> CacheDict = new ConcurrentDictionary<string, ControlMapping>();

        /// <summary>
        /// 获取用户控件类型
        /// </summary>
        /// <param name="targetName">目标控件唯一名称，取目标对象全名（FullName）</param>
        /// <returns>返回控件对应类型</returns>
        public ControlMapping GetControl(string targetName)
        {
            return CacheDict.FirstOrDefault(x => x.Value.TargetType.FullName == targetName).Value;
        }

        /// <summary>
        /// 获取指定窗体或Page可以加载到指定区域的控件集合
        /// </summary>
        /// <param name="currentFullName">当前窗体或Page对象全名(FullName)</param>
        /// <returns></returns>
        public List<ControlMapping> GetControls(string currentFullName)
        {
            return CacheDict.Where(x => x.Value.TargetType.FullName == currentFullName).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// 添加Page页面类型到字典
        /// </summary>
        /// <param name="currentFullName">当前控件名称</param>
        /// <param name="mapping">转换关系</param>
        public void Add(string currentFullName, ControlMapping mapping)
        {
            if (CacheDict.ContainsKey(currentFullName)) throw new Exception($"初始化系统占位控件出错，存在重复的名称为【{currentFullName}】控件对象！");
            CacheDict.TryAdd(currentFullName, mapping);
        }
    }
}
