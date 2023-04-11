using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.ViewModels
{
	public class HeaderItemViewModel
	{
		public string Key { get; set; }
		public string Value { get; set; }


		public HeaderItemViewModel(string key, string value) { this.Key = key; this.Value = value; }

		public override string ToString() { return $"{this.Key}:{this.Value}\n"; }
	}
}
