using Avalonia;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CacheProxyMockServer.Models;

namespace CacheProxyMockServer
{
	internal class Program
	{
		// Initialization code. Don't use any Avalonia, third-party APIs or any
		// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
		// yet and stuff might break.
		[STAThread]
		public static void Main(string[] args)
		{

			// prevent more than one app instance
			//
			if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
			{
				return;
			}

			// init the db
			//
			//using (var db = new AppDbContext())
			//{
			//	db.Database.EnsureCreated();
			//	db.Database.Migrate();
			//}

			// run the server
			//
			Task.Run(() => {
				// server
				var host = new WebHostBuilder()
					.UseKestrel()
					.UseUrls("http://*:1234")
					.UseStartup<Startup>()
					.Build();

				host.Run();
			});

			// avalonia ui
			//
			BuildAvaloniaApp()
			.StartWithClassicDesktopLifetime(args);
		}

		// Avalonia configuration, don't remove; also used by visual designer.
		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace();


		static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args);
			
		}
	}
}