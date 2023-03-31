using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace CacheProxyMockServer.Http
{
	public class HttpService : IHttpService
	{

		public HttpService()
		{

		}

		public Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
		{
			using (var client = new HttpClient())
			{
				client.SendAsync();
			}

		}


	}

}

