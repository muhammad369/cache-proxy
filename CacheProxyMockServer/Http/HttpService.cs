using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using HttpMethod = System.Net.Http.HttpMethod;

namespace CacheProxyMockServer.Http
{
	public class HttpService : IHttpService
	{
		readonly HttpClient _httpClient;

		public HttpService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
		{

			try
			{
				var request1 = new HttpRequestMessage(HttpMethod.Get, "https://google.com");
				return _httpClient.SendAsync(request1);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to send request due to exception: " + ex.Message + "\r\n" +
					$"url: {request.RequestUri} \r\nmethod: {request.Method} \r\ncontent: {request.Content}")
				{ };
			}


		}


	}

}

