using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Modules
{
    /// <summary>
    /// 页面管理器
    /// </summary>
    public class PageManager
    {
        private static readonly ConcurrentDictionary<string, Type> CacheDict = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 获取Page页面类型
        /// </summary>
        /// <param name="path">功能菜单唯一路径</param>
        /// <returns>返回Page对应类型</returns>
        public Type GetPage(string path)
        {
            if (CacheDict.ContainsKey(path))
                return CacheDict[path];
            return null;
        }

        /// <summary>
        /// 添加Page页面类型到字典
        /// </summary>
        /// <param name="path"></param>
        /// <param name="page"></param>
        public void Add(string path, Type page)
        {
            if (CacheDict.ContainsKey(path)) throw new Exception($"初始化系统功能菜单出错，存在重复的名称为【{path}】的Page或窗体对象");
            CacheDict.TryAdd(path, page);
        }
    }
}
