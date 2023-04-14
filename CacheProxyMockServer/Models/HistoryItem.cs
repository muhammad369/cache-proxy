
using CacheProxyMockServer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(MatchedRuleId))]
	[Index(nameof(Time))]
	public class HistoryItem
	{
		public int Id { get; set; }

		public string Method { get; set; }
		public string Url { get; set; }

		public string RequestContent { get; set; }
		public string RequestHeaders { get; set; }


		public int ResponseStatus { get; set; }
		public string? ResponseReason { get; set; }
		public string ResponseContentType { get; set; }
		public string ResponseHeaders { get; set; }
		public string ResponseContent { get; set; }

		public DateTime Time { get; set; }

		public bool FromCache { get; set; }

		public int MatchedRuleId { get; set; }
		public Rule MatchedRule { get; set; }
	}

	public static class HistoryItemExtensions
	{
		public static HistoryItem FromRule(this HistoryItem h, Rule rule)
		{
			h.Method = rule.Method;
			h.Url = rule.Url;
			h.RequestContent = rule.RequestBody;
			h.ResponseStatus = rule.ResponseStatus;
			h.ResponseReason = rule.ResponseReason;
			h.ResponseContentType = rule.ResponseContentType;
			h.ResponseContent = rule.ResponseContent;
			h.ResponseHeaders = rule.ResponseHeaders;
			//h.MatchedRuleId = rule.Id;
			h.MatchedRule = rule;
			//
			return h;
		}

		public static RequestDetailsViewModel ToViewModel(this HistoryItem h)
		{
			var vm = new RequestDetailsViewModel();
			vm.Id = h.Id;
			vm.Method = h.Method;
			vm.Url = h.Url;
			vm.RequestBody = h.RequestContent;
			vm.RequestHeaders = h.RequestHeaders;
			vm.ResponseContent = h.ResponseContent;
			vm.ResponseReason = h.ResponseReason ?? "";
			vm.ResponseHeaders = h.ResponseHeaders;
			vm.ResponseStatus = h.ResponseStatus;
			return vm;
		}
	}

}
