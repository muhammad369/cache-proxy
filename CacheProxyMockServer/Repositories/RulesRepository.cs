﻿using CacheProxyMockServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public class RulesRepository : GenericRepository<Rule>
	{
		public RulesRepository(DbContext dc) : base(dc)
		{
		}

		public async Task<Rule?> GetMatchedRule(HttpRequestMessage request)
		{
			
			var content = await request.Content.ReadAsStringAsync();
			return GetAll().FirstOrDefault(r => r.IsActive 
								&& r.Method == request.Method.Method 
								&& r.Url == request.RequestUri.AbsoluteUri 
								&& (r.RequestBody ?? "") == content
								);
		}
	}
}