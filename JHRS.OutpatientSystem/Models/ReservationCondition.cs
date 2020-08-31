using JHRS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.OutpatientSystem.Models
{
	public class ReservationCondition
	{
		[QueryRule(FilterOperate.Contains, "Index", "", FilterOperate.And)]
		public string Index
		{
			get;
			set;
		}

		[QueryRule(FilterOperate.Contains, "DeptID", "", FilterOperate.And)]
		public int DepartmentID
		{
			get;
			set;
		}

		[QueryRule(FilterOperate.Contains, "DictionaryID", "", FilterOperate.And)]
		public int DicID
		{
			get;
			set;
		}
	}
}
