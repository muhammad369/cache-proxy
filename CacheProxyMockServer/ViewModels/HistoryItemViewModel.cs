using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.ViewModels
{
	public class HistoryItemViewModel : ViewModelBase
	{

		public HistoryItemViewModel() { }

		public int Id { get; set; }

		public string Method { get; set; }
		public string Url { get; set; }

		public string Time { get; set; }

		public bool FromCache { get; set; }

		public int MatchedRuleId { get; set; }


		public void OpenRuleWindow()
		{

		}

		public void ShowDetails()
		{

		}
	}
}
