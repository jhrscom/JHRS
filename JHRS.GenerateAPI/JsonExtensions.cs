using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JHRS.GenerateAPI
{
	public static class JsonExtensions
	{
		public static string JsonDateTimeFormat(string json)
		{
			json = Regex.Replace(json, "\\\\/Date\\((\\d+)\\)\\\\/", (MatchEvaluator)((Match match) => new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(match.Groups[1].Value)).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff")));
			return json;
		}

		public static string ToJson(this object @object, bool camelCase = false, bool indented = false)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			if (camelCase)
			{
				settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}
			if (indented)
			{
				settings.Formatting = Formatting.Indented;
			}
			string json = JsonConvert.SerializeObject(@object, settings);
			return JsonDateTimeFormat(json);
		}

		public static T FromJsonString<T>(this string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static object FromJsonString(this string json, Type type)
		{
			return JsonConvert.DeserializeObject(json, type);
		}
	}

}
