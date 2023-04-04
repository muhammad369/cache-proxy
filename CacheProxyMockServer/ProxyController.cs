using CacheProxyMockServer.Http;
using CacheProxyMockServer.Models;
using CacheProxyMockServer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CacheProxyMockServer
{
	public class ProxyController : ControllerBase
	{
		/// <summary>
		/// Is proxy or cache
		/// </summary>
		public static bool IsProxy = false;
		/// <summary>
		/// Simulated delay in seconds
		/// </summary>
		public static int SimulatedDelay = 2;

		//
		readonly IHttpService http;
		readonly UnitOfWork uow;

		public ProxyController(IHttpService http, UnitOfWork uow)
		{
			this.http = http;
			this.uow = uow;
		}


		[HttpGet, HttpPost, HttpPatch, HttpPut, HttpOptions, HttpOptions, HttpDelete, HttpHead]
		[Route("/")]
		public async Task<IActionResult> Home([FromQuery(Name = "url")] string url)
		{
			try
			{
				var urlParts = UrlHelpers.SplitUrl(url);

				//
				// check server rename
				//

				var rename = uow.ServerRenamesRepo.GetRenameFor(urlParts[1]);

				if (rename != null)
				{
					url = $"{urlParts[0]}//{rename}/{urlParts[2]}";
				}

				var request = Request.ToHttpRequestMessage();
				request.RequestUri = new Uri(url);

				//
				// get the response
				//

				IActionResult resp = null;
				var matchedRule = await uow.RulesRepo.GetMatchedRule(request);
				Rule newRule = null;
				bool fromCache = false;

				if (IsProxy || matchedRule == null)
				{
					var r = await http.sendRequest(request);
					resp = new HttpResponseMessageResult(r);
					// new rule
					newRule = await new Rule().FromRequestResponseAsync(request, r);
				}
				else
				{
					if (SimulatedDelay > 0)
					{
						await Task.Delay(SimulatedDelay * 1000);
					}
					// load response from cache (the matched rule)
					resp = matchedRule.ToResponse();
					fromCache = true;
				}

				//
				// add a rule and history
				//

				if (matchedRule == null)
				{
					// add a new rule
					uow.RulesRepo.Add(newRule);
				}

				// add a history item
				var hi = new HistoryItem() 
				{ 
					FromCache = fromCache, Time = DateTime.Now, 
					RequestHeaders = request.GetHeadersString() 
				}
				.FromRule(matchedRule ?? newRule);
				uow.HistoryItemsRepo.Add(hi);
				uow.Save();

				// return the response

				return resp;
			}
			catch(Exception ex)
			{
				//return new OkResult();
				await Response.WriteAsync($"title: error from CacheProxyMockServer, exception: {ex}");
				return new Microsoft.AspNetCore.Mvc.StatusCodeResult(500) { };
			}
		}

		[HttpGet, HttpPost, HttpPatch, HttpPut, HttpOptions, HttpOptions, HttpDelete, HttpHead]
		[Route("test")]
		public async Task<IActionResult> test()
		{
			return new JsonResult("Test");

		}

	}
}
