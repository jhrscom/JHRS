using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.GenerateAPI
{
	public class Method
	{
		public string MethodType
		{
			get;
			set;
		}

		public string Api
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

		public string[] ParameterType
		{
			get;
			set;
		}

		public string[] ParameterName
		{
			get;
			set;
		}

		public string[] ParameterDes
		{
			get;
			set;
		}

		public string Return
		{
			get;
			set;
		}
	}

}
