﻿using CacheProxyMockServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public class UnitOfWork
	{
		private static AppDbContext dc;

		public HistoryItemsRepository HistoryItemsRepo { get; }
		public RulesRepository RulesRepo { get; }
		public ServerRenameRepository ServerRenamesRepo { get; }
		public SettingsRepository SettingsRepo { get; }


		public UnitOfWork()//AppDbContext dc)
		{
			if(dc == null) dc = new AppDbContext();
			//
			this.HistoryItemsRepo = new HistoryItemsRepository(dc);
			this.RulesRepo = new RulesRepository(dc);
			this.ServerRenamesRepo = new ServerRenameRepository(dc);
			this.SettingsRepo = new SettingsRepository(dc);
		}

		public void Save()
		{
			dc.SaveChanges();
		}

	}
}
