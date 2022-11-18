
using System.Collections.Generic;

namespace CacheProxyMockServer.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			
			historyItems.Add(new HistoryItemViewModel() { Id = 34, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache=true });
			historyItems.Add(new HistoryItemViewModel() { Id = 44, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });
			historyItems.Add(new HistoryItemViewModel() { Id = 54, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });
			historyItems.Add(new HistoryItemViewModel() { Id = 74, Time = "Now", Method = "Get", Url = "http://fgf.com/df/dfdf/dfd", FromCache = true });

		}

		public List<HistoryItemViewModel> historyItems { get; set; } = new List<HistoryItemViewModel>();
	}
}