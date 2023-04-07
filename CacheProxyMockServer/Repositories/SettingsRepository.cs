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

		static string delayKey = "delay";
		static string modeKey = "mode";

		public SettingsRepository(DbContext dc) : base(dc)
		{

		}

		public string? getValue(string key)
		{
			return FindSingle(s=> s.Name == key)?.Value;
		}

		public void setValue(string key, string value)
		{
			var entity = FindSingle(s => s.Name == key);
			if (entity != null) 
			{ 
				entity.Value = value;
				Update(entity);
			}
			else
			{
				entity = new Setting { Name = key, Value= value };
				Add(entity);
			}
		}

		public int getDelay()
		{
			return int.Parse(getValue(delayKey) ?? "2");
		}

		public void setDelay(int delay)
		{
			setValue(delayKey, delay.ToString());
		}

		public bool getMode()
		{
			return (getValue(modeKey) ?? "1") == "1";
		}

		public void setMode(bool isProxy)
		{
			setValue(modeKey, isProxy? "1":"0");
		}

	}
}
