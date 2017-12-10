using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cache_proxy
{
    public class CustomHeadersConfig : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith((task) =>
                {
                    HttpResponseMessage response = task.Result;
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.Headers.Add("Access-Control-Allow-Methods", "*, GET, PUT, POST, DELETE, HEAD, OPTIONS");
                    response.Headers.Add("Access-Control-Allow-Headers", "*, sessiontoken, Origin, X-Requested-With, Content-Type, Accept, SOAPAction");
                    return response;
                });
        }
    }
}
