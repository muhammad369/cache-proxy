using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Http
{
	public interface IHttpService
	{
		Task<HttpResponseMessage> sendRequest(HttpRequestMessage request);
	}

	public static class HttpMethods
	{
		public const string Get = "GET";
		public const string Post = "POST";
		public const string Put = "PUT";
		public const string Patch = "PATCH";
		public const string Delete = "DELETE";
		public const string Head = "HEAD";
		public const string Options = "OPTIONS";
		public const string Trace = "TRACE";
		//public const string Connect = "CONNECT";
	}



}
