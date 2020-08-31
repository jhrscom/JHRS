using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Enums
{
    /// <summary>
    /// 圖標
    /// </summary>
    public enum IconEnum
    {
        /// <summary>
        /// 詳情頁面圖標
        /// </summary>
        [Description("detail")]
        詳情頁面圖標,

        /// <summary>
        /// 新增頁面圖標
        /// </summary>
        [Description("add")]
        新增頁面圖標,

        /// <summary>
        /// 編輯頁面圖標
        /// </summary>
        [Description("edit")]
        編輯頁面圖標
    }
}
