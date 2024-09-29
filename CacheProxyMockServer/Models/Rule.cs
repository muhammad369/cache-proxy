using CacheProxyMockServer.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(Method), nameof(Url), nameof(RequestBody), 
		nameof(IsActive))]
	public class Rule
	{
		public int Id { get; set; }

		public string? Method { get; set; }
		public string Url { get; set; }
		public string? RequestBody { get; set; }

		public bool IsActive { get; set; } = true;
		//public bool IsUrlTemplate { get; set; } = false;
		//public bool IsBodyTemplate { get; set; } = false;

		public int ResponseStatus { get; set; }
		public string? ResponseReason { get; set; }
		public string ResponseContent { get; set; }
		public string ResponseContentType { get; set; }
		public string ResponseHeaders { get; set; }

		public ICollection<HistoryItem> HistoryItems { get; set; }
	}

	public static class RuleExtensions
	{
		public static IActionResult ToResponse(this Rule rule)
		{
			return new CachedResponseResult(rule);
			
		}

		public static async Task<Rule> FromRequestResponseAsync(this Rule rule, HttpRequestMessage req, HttpResponseMessage resp)
		{
			rule.Method = req.Method.ToString();
			rule.Url = req.RequestUri?.AbsoluteUri;
			rule.IsActive = true;
			rule.RequestBody = await req.Content?.ReadAsStringAsync();
			rule.ResponseStatus = (int)resp.StatusCode;
			rule.ResponseReason = resp.ReasonPhrase;
			rule.ResponseContent = await resp.Content.ReadAsStringAsync();
			rule.ResponseContentType = resp.Content.Headers.ContentType?.ToString();
			rule.ResponseHeaders = resp.GetHeadersString();
			//
			return rule;
		}

	}

}
