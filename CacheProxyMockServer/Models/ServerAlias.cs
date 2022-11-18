using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Models
{
	[Index(nameof(Name), nameof(IsActive))]
	[Index(nameof(Name), IsUnique = true)]
	public class ServerAlias
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string Alias { get; set; }

		public bool IsActive { get; set; } = true;
	}
}
