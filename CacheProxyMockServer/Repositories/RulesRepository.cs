using CacheProxyMockServer.Models;
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

		public int GetCount(string searchUrl)
		{
			return GetAll().Where(r=> r.Url.Contains(searchUrl)).Count();
		}

		public List<Rule> Search(string  searchUrl, int pgNumber, int pgSize)
		{
			return FindBy(r => r.Url.Contains(searchUrl), pgNumber, pgSize).ToList();
		}
	}
}
