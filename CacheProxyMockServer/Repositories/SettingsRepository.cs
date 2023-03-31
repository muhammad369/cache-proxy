using CacheProxyMockServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public class SettingsRepository : GenericRepository<Setting>
	{
		public SettingsRepository(DbContext dc) : base(dc)
		{
		}
	}
}
