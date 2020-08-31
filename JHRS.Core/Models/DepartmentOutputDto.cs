using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Models
{
    /// <summary>
    /// 科室信息(web api返回數據)
    /// </summary>
    public class DepartmentOutputDto
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 科室名稱
        /// </summary>
        public string Name { get; set; }
    }
}
