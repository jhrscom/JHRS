using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.GenerateAPI
{
	public class CollectEntityClass
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

		public List<EntityProperty> Properties
		{
			get;
			set;
		}
	}

}
