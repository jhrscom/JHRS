using JHRS.Core.Enums;
using JHRS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Models
{
    /// <summary>
    /// 預約掛號記錄輸出對象(界面展示)
    /// </summary>
    public class ReservationOutputDto
    {
        /// <summary>
        /// 記錄ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 叫號順序
        /// </summary>
        [BindDescription("叫號順序", ShowScheme.普通文本, "80*", 1, "")]
        public string Index { get; set; }

        /// <summary>
        /// 流水號
        /// </summary>
        [BindDescription("流水號", ShowScheme.普通文本, "100*", 2, "")]
        public string BusinessNumber { get; set; }

        /// <summary>
        /// 掛號狀態
        /// </summary>
        [BindDescription("掛號狀態", ShowScheme.自定义, "100*", 7, "Status")]
        public EntityStatus Status { get; set; }

        /// <summary>
        /// 掛號時間
        /// </summary>
        [BindDescription("掛號時間", ShowScheme.普通文本, "150*", 8, "")]
        public DateTime ReservationTime { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        [BindDescription("病人姓名", ShowScheme.普通文本, "80*", 3, "")]
        public string Name { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [BindDescription("性別", ShowScheme.普通文本, "80*", 4, "")]
        public int Gender { get; set; }

        /// <summary>
        /// 掛號科室
        /// </summary>
        [BindDescription("掛號科室", ShowScheme.普通文本, "100*", 4, "")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 診治醫師
        /// </summary>
        [BindDescription("診治醫師", ShowScheme.普通文本, "80*", 5, "")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [BindDescription("有效期", ShowScheme.普通文本, "100*", 6, "")]
        public string Expire { get; set; }
    }
}