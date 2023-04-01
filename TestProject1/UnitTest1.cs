using CacheProxyMockServer.Http;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TestProject1
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void url_helpers()
		{
			var url = "https://server.com:port/qqqq/asdf?grrrf=mkmjnd&gff=ok";

			var parts = UrlHelpers.SplitUrl(url);


			Assert.Equals("https:", parts[0]);
			Assert.Equals("server.com:port", parts[1]);
			Assert.Equals("qqqq/asdf?grrrf=mkmjnd&gff=ok", parts[2]);
		}



		[Test]
		public async Task fetch_google_page()
		{
			var url = "https://google.com";

			var httpclient = new HttpClient();
			httpclient.BaseAddress = new Uri(url);
			var response = await httpclient.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();
		}
	}
}