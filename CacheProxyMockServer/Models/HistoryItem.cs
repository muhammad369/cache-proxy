
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(MatchedRuleId))]
	public class HistoryItem
	{
		public int Id { get; set; }

		public string Method { get; set; }
		public string Url { get; set; }

		public string RequestContent { get; set; }
		public string RequestHeaders { get; set; }


		public int ResponseStatus { get; set; }
		public string ResponseHeaders { get; set; }
		public string ResponseContent { get; set; }

		public DateTime Time { get; set; }

		public bool FromCache { get; set; }

		public int MatchedRuleId { get; set; }
		public Rule MatchedRule { get; set; }
	}
}
