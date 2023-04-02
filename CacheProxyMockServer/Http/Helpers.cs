using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using CacheProxyMockServer.Models;
using System.Net.Http.Headers;

namespace CacheProxyMockServer.Http
{
	public static class RequestTranscriptHelpers
	{
		public static HttpRequestMessage ToHttpRequestMessage(this HttpRequest req)
			=> new HttpRequestMessage()
				.SetMethod(req)
				.SetAbsoluteUri(req)
				.SetHeaders(req)
				.SetContent(req)
				.SetContentType(req);

		private static HttpRequestMessage SetAbsoluteUri(this HttpRequestMessage msg, HttpRequest req)
			=> msg.Set(m => m.RequestUri = new UriBuilder
			{
				Scheme = req.Scheme,
				Host = req.Host.Host,
				Port = req.Host.Port.Value,
				Path = req.PathBase.Add(req.Path),
				Query = req.QueryString.ToString()
			}.Uri);

		private static HttpRequestMessage SetMethod(this HttpRequestMessage msg, HttpRequest req)
			=> msg.Set(m => m.Method = new HttpMethod(req.Method));

		private static HttpRequestMessage SetHeaders(this HttpRequestMessage msg, HttpRequest req)
			=> req.Headers.Aggregate(msg, (acc, h) => acc.Set(m => m.Headers.TryAddWithoutValidation(h.Key, h.Value.AsEnumerable())));

		private static HttpRequestMessage SetContent(this HttpRequestMessage msg, HttpRequest req)
			=> msg.Set(m => m.Content = new StreamContent(req.Body));

		private static HttpRequestMessage SetContentType(this HttpRequestMessage msg, HttpRequest req)
			=> msg.Set(m => m.Content.Headers.Add("Content-Type", req.ContentType), applyIf: req.Headers.ContainsKey("Content-Type"));

		private static HttpRequestMessage Set(this HttpRequestMessage msg, Action<HttpRequestMessage> config, bool applyIf = true)
		{
			if (applyIf)
			{
				config.Invoke(msg);
			}

			return msg;
		}
	}



	public static class ResponseTranscriptHelpers
	{
		public static async Task FromHttpResponseMessage(this HttpResponse resp, HttpResponseMessage msg)
		{
			resp.SetStatusCode(msg)
				.SetHeaders(msg)
				.SetContentType(msg);

			await resp.SetBodyAsync(msg);
		}

		private static HttpResponse SetStatusCode(this HttpResponse resp, HttpResponseMessage msg)
			=> resp.Set(r => r.StatusCode = (int)msg.StatusCode);

		private static HttpResponse SetHeaders(this HttpResponse resp, HttpResponseMessage msg)
			=> msg.Headers.Aggregate(resp, (acc, h) => acc.Set(r => r.Headers[h.Key] = new StringValues(h.Value.ToArray())));

		private static async Task<HttpResponse> SetBodyAsync(this HttpResponse resp, HttpResponseMessage msg)
		{
			using (var stream = await msg.Content.ReadAsStreamAsync())
			using (var reader = new StreamReader(stream))
			{
				var content = await reader.ReadToEndAsync();

				return resp.Set(async r => await r.WriteAsync(content));
			}
		}

		private static HttpResponse SetContentType(this HttpResponse resp, HttpResponseMessage msg)
			=> resp.Set(r => r.ContentType = msg.Content.Headers.GetValues("Content-Type").Single(), applyIf: msg.Content.Headers.Contains("Content-Type"));

		private static HttpResponse Set(this HttpResponse msg, Action<HttpResponse> config, bool applyIf = true)
		{
			if (applyIf)
			{
				config.Invoke(msg);
			}

			return msg;
		}
	}


	public static class UrlHelpers
	{
		/// <summary>
		/// 0 => protocol <para/>
		/// 1 => server address <para/>
		/// 2 => path
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string[] SplitUrl(string url)
		{
			var arr = new string[3];
			var url1 = url.Split("//");
			arr[0] = url1[0];
			// TODO: take index of '?'
			var slashIndex = url1[1].IndexOf('/');
			if (slashIndex == -1)
			{
				arr[1] = url1[1];
				arr[2] = "";
			}
			else
			{
				arr[1] = url1[1].Substring(0, slashIndex);
				arr[2] = url1[1].Substring(slashIndex + 1);

			}
			//
			return arr;
		}



	}

	public static class HeadersHelpers
	{
		public static string GetHeadersString(this HttpRequestMessage req)
		{
			return string.Join("\n", req.Headers.Select(h => $"{h.Key}:{string.Join(",", h.Value)}"));
		}

		public static string GetHeadersString(this HttpResponseMessage resp)
		{
			return string.Join("\n", resp.Headers.Select(h => $"{h.Key}:{string.Join(",", h.Value)}"));
		}

	}

	public class HttpResponseMessageResult : IActionResult
	{
		private readonly HttpResponseMessage _responseMessage;

		public HttpResponseMessageResult(HttpResponseMessage responseMessage)
		{
			_responseMessage = responseMessage; // could add throw if null
		}

		public async Task ExecuteResultAsync(ActionContext context)
		{
			var response = context.HttpContext.Response;


			if (_responseMessage == null)
			{
				var message = "Response message cannot be null";

				throw new InvalidOperationException(message);
			}

			using (_responseMessage)
			{
				response.StatusCode = (int)_responseMessage.StatusCode;

				var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
				if (responseFeature != null)
				{
					responseFeature.ReasonPhrase = _responseMessage.ReasonPhrase;
				}

				var responseHeaders = _responseMessage.Headers;

				// Ignore the Transfer-Encoding header if it is just "chunked".
				// We let the host decide about whether the response should be chunked or not.
				if (responseHeaders.TransferEncodingChunked == true &&
					responseHeaders.TransferEncoding.Count == 1)
				{
					responseHeaders.TransferEncoding.Clear();
				}

				foreach (var header in responseHeaders)
				{
					response.Headers.Append(header.Key, header.Value.ToArray());
				}

				if (_responseMessage.Content != null)
				{
					var contentHeaders = _responseMessage.Content.Headers;

					// Copy the response content headers only after ensuring they are complete.
					// We ask for Content-Length first because HttpContent lazily computes this
					// and only afterwards writes the value into the content headers.
					var unused = contentHeaders.ContentLength;

					foreach (var header in contentHeaders)
					{
						response.Headers.Append(header.Key, header.Value.ToArray());
					}

					await _responseMessage.Content.CopyToAsync(response.Body);
				}
			}
		}

	}

	public class CachedResponseResult : IActionResult
	{
		private readonly Rule _rule;

		public CachedResponseResult(Rule rule)
		{
			_rule = rule; // could add throw if null
		}

		public async Task ExecuteResultAsync(ActionContext context)
		{
			var response = context.HttpContext.Response;


			if (_rule == null)
			{
				var message = "Response message cannot be null";

				throw new InvalidOperationException(message);
			}


			response.StatusCode = _rule.ResponseStatus;

			var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
			if (responseFeature != null)
			{
				responseFeature.ReasonPhrase = _rule.ResponseReason;
			}

			var responseHeaders = _rule.ResponseHeaders;



			foreach (var header in responseHeaders.Split("\n"))
			{
				var headerParts = header.Split(":");
				response.Headers.Append(headerParts[0], headerParts[1]);
			}

			if (_rule.ResponseContent != null)
			{
				var bytes = Encoding.UTF8.GetBytes(_rule.ResponseContent);

				response.ContentType = _rule.ResponseContentType;
				response.ContentLength = bytes.Length;


				await response.Body.WriteAsync(bytes, 0, bytes.Length);
			}

		}

	}

}