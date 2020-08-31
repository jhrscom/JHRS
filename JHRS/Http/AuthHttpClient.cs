using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Http
{
	/// <summary>
	/// 已授权HttpClient对象
	/// </summary>
	public class AuthHttpClient
	{
		private static readonly Lazy<HttpClient> InstanceLazy = new Lazy<HttpClient>(() => new HttpClient());

		public static HttpClient Instance => InstanceLazy.Value;

		private AuthHttpClient()
		{
		}

		public static void SetHttpClient(string baseUrl, string token)
		{
			InstanceLazy.Value.BaseAddress = new Uri(baseUrl);
			InstanceLazy.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
