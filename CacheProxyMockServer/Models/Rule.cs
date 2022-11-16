using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	public class Rule
	{
		public int Id { get; set; }


		public string? Method { get; set; }

		public string? RequestContent { get; set; }


		public int ResponseStatus { get; set; }
		public string ResponseContent { get; set; }

		public bool IsActive { get; set; } = true;
		public bool IsTemplate { get; set; } = false;

		public ICollection<HistoryItem> HistoryItems { get; set; }
	}
}
