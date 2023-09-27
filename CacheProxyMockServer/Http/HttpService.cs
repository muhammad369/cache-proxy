using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using HttpMethod = System.Net.Http.HttpMethod;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;

namespace CacheProxyMockServer.Http
{
	public class HttpService : IHttpService
	{
		readonly HttpClient _httpClient;

		public HttpService()
		{
			HttpClientHandler handler = new HttpClientHandler()
			{
				SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
			};
			handler.ServerCertificateCustomValidationCallback = ValidateServerCertificate;
			handler.ClientCertificateOptions = ClientCertificateOption.Manual;
			handler.ClientCertificates.Add(new X509Certificate2("cert.crt"));

			_httpClient = new HttpClient(handler);

			//System.Net.ServicePointManager.SecurityProtocol =
			//	SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

			ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
		}

		private static bool ValidateServerCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				// Ignore the certificate name mismatch error
				return true;
			}
			else
			{
				// Perform default certificate validation
				return sslPolicyErrors == SslPolicyErrors.None;
			}
		}

		

		public Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
		{

			try
			{
				System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

				return _httpClient.SendAsync(request);
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

