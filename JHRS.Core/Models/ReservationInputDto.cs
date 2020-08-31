using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Models
{
    /// <summary>
    /// 預約掛號輸入DTO
    /// </summary>
    public class ReservationInputDto
    {
        /// <summary>
        /// 叫號順序
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 流水號
        /// </summary>
        public string BusinessNumber { get; set; }

        /// <summary>
        /// 掛號時間
        /// </summary>
        public DateTime ReservationTime { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 掛號科室
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 診治醫師
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string Expire { get; set; }
    }
}