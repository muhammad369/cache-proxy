using CacheProxyMockServer.Http;
using CacheProxyMockServer.Models;
using CacheProxyMockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer
{
	public class Startup
	{
		
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddControllers();
			services.AddHttpClient();
			//
			services.AddScoped<IHttpService, HttpService>();
			services.AddScoped<UnitOfWork, UnitOfWork>();
			//
			//services.AddDbContext<AppDbContext>(options=> options.UseInMemoryDatabase("db"));
			//services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=./db.sqlite"));
			services.AddDbContext<AppDbContext>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseDeveloperExceptionPage();
		}
	}
}