using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Identity
{
    /// <summary>
    /// 登錄用戶上下文
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用戶所在科室
        /// </summary>
        public int[] DepartmentID { get; set; }

        /// <summary>
        /// 用戶當前所在科室
        /// </summary>
        public int CurrentDepartmentID { get; set; }

        /// <summary>
        /// 用戶當前所在科室名稱
        /// </summary>
        public string CurrentDepartmentName { get; set; } = "重症醫學科（ICU）";

        /// <summary>
        /// 當前登錄用戶姓名
        /// </summary>
        public string UserName { get; set; } = "趙佳仁";

        /// <summary>
        /// 當前用戶角色ID集合
        /// </summary>
        public int[] RoleID { get; set; }

        /// <summary>
        /// 用戶會話token
        /// </summary>
        public UserToken Token { get; set; }

        /// <summary>
        /// 醫院名稱
        /// </summary>
        public string HospitalName { get; set; } = "江湖人士醫院";

        /// <summary>
        /// 顯示
        /// </summary>
        public string ShowText => "用戶：" + UserName + "，科室：【" + CurrentDepartmentName + "】";
    }
}