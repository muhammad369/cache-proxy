using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CacheProxyMockServer
{
	public class ProxyController: ControllerBase
	{
		[HttpGet]
		[Route("/")]
		public string Get()
		{
			return "Test";
		}
	}
}
