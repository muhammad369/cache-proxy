using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	public class AppDbContext : DbContext
	{
		//public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		//{

		//}

		public AppDbContext() : base()
		{

		}

		public virtual DbSet<HistoryItem> HistoryItems { get; set; }
		public virtual DbSet<Rule> Rules { get; set; }
		public virtual DbSet<ServerRename> ServerRenames { get; set; }
		public virtual DbSet<Setting> Settings { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=./db.sqlite");
		}

	}

}
