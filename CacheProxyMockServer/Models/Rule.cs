﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(Method), nameof(Url), nameof(RequestBody), 
		nameof(IsActive), nameof(IsBodyTemplate), nameof(IsUrlTemplate))]
	public class Rule
	{
		public int Id { get; set; }

		public string? Method { get; set; }
		public string Url { get; set; }
		public string? RequestBody { get; set; }

		public bool IsActive { get; set; } = true;
		public bool IsUrlTemplate { get; set; } = false;
		public bool IsBodyTemplate { get; set; } = false;

		public int ResponseStatus { get; set; }
		public string ResponseContent { get; set; }
		public string ResponseHeaders { get; set; }

		public ICollection<HistoryItem> HistoryItems { get; set; }
	}
}
