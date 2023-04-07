using CacheProxyMockServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.ViewModels
{
	public class HistoryItemViewModel
	{

		public HistoryItemViewModel(HistoryItem model) 
		{
			this.Id = model.Id;
			this.Url = model.Url;
			this.Method = model.Method;
			this.Time = model.Time.ToString("yyyy-MM-dd hh:mm");
			this.FromCache = model.FromCache;
			this.MatchedRuleId = model.MatchedRuleId;
		}

		public int Id { get; set; }

		public string Method { get; set; }
		public string Url { get; set; }

		public string Time { get; set; }

		public bool FromCache { get; set; }

		public int MatchedRuleId { get; set; }
	}
}
