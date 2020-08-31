using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.ViewModels
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success,
        /// <summary>
        /// 操作错误
        /// </summary>
        Error,
        /// <summary>
        /// 再次确认
        /// </summary>
        Question
    }
}
