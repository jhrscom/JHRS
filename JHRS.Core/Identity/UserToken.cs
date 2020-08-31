using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Identity
{
    /// <summary>
    /// 會話token
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// 訪客token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 過期時間
        /// </summary>
        public long RefreshUctExpires { get; set; }
    }
}
