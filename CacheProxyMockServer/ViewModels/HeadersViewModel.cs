using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.ViewModels
{
	public class HeadersViewModel: ViewModelBase
	{

		public List<KeyValuePair<string, string>> Headers { get; set; }

		public HeadersViewModel() { }
	}
}
