using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(Name))]
	public class Setting
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string Value { get; set; }
	}
}
