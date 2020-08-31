using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.GenerateAPI
{
	public class CollectEnumClass
	{
		public string Module
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public List<EnumProperty> Properties
		{
			get;
			set;
		}
	}
}
