using CacheProxyMockServer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public class HistoryItemsRepository : GenericRepository<HistoryItem>
	{
		public HistoryItemsRepository(DbContext dc) : base(dc)
		{
			
		}


		public List<HistoryItem> GetPage(int pageIndex, int pageSize)
		{
			return GetAll().OrderByDescending(h => h.Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
		}

		public int GetCount()
		{
			return GetAll().Count();
		}

	}
}
