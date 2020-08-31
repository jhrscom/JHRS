using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Data
{
    /// <summary>
    /// 业务操作结果接口
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public interface IOperationResult<TResultType, TData>
    {
        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        TData Data { get; set; }
    }

    /// <summary>
    /// 操作结果
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public interface IOperationResult<TResultType> : IOperationResult<TResultType, object>
    { }
}
