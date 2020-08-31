using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.GenerateAPI
{
	public class EntityProperty
	{
		public string PropertyType
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

		public string IsNullable
		{
			get;
			set;
		}

		public string Format
		{
			get;
			set;
		}
	}
}
