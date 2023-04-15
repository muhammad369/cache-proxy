using CacheProxyMockServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.ViewModels
{
	public class RuleItemViewModel
	{

		public RuleItemViewModel() { }

		public RuleItemViewModel(Rule model) 
		{
			Id = model.Id;
			Method = model.Method;
			Url = model.Url;
			RequestBody = model.RequestBody;
			IsActive = model.IsActive;
		}

		public int Id { get; set; }

		public string Method { get; set; }
		public string Url { get; set; }
		public string RequestBody { get; set; }

		public bool IsActive { get; set; } = true;
		//public bool IsUrlTemplate { get; set; } = false;
		//public bool IsBodyTemplate { get; set; } = false;

		//public int ResponseStatus { get; set; }
		//public string ResponseContent { get; set; }
		//public string ResponseHeaders { get; set; }
	}
}
