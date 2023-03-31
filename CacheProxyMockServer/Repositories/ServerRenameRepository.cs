using CacheProxyMockServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public class ServerRenameRepository : GenericRepository<ServerRename>
	{
		public ServerRenameRepository(DbContext dc) : base(dc)
		{
		}

		public string? GetRenameFor(string serverName)
		{
			return FindSingle(a => a.IsActive && a.Name == serverName)?.Rename;
		}
	}
}
