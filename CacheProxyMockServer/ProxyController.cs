using CacheProxyMockServer.Http;
using CacheProxyMockServer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CacheProxyMockServer
{
	public class ProxyController: ControllerBase
	{
		/// <summary>
		/// Is proxy or cache
		/// </summary>
		public static bool IsProxy = true;
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
		public async IActionResult Home([FromQuery(Name = "url")] string url)
		{
			var request = Request.ToHttpRequestMessage();
			var urlParts = UrlHelpers.SplitUrl(url);

			//
			// check server rename
			//

			var rename = uow.ServerRenamesRepo.GetRenameFor(urlParts[1]);

			if(rename != null)
			{
				url = $"{urlParts[0]}//{rename}/{urlParts[2]}";
			}

			request.RequestUri = new Uri(url);

			//
			// get the response
			//

			HttpResponseMessage resp;
			var matchedRule = uow.RulesRepo.GetMatchedRule(request);

			if (IsProxy || matchedRule == null)
			{
				resp = await http.sendRequest(request);
			}
			else
			{
				if (SimulatedDelay > 0)
				{
					await Task.Delay(SimulatedDelay * 1000);
				}
				// load response from cache (the matched rule)

			}

			//
			// add a rule and history
			//

			if(matchedRule == null)
			{
				// add a new rule

			}

			// add a history item


			// return the response

			return new JsonResult("");
		}
	}
}
