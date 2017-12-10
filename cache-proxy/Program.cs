using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace cache_proxy
{
    static class Program
    {

        public static bool useCache = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                return;
            }

            var config = new HttpSelfHostConfiguration("http://localhost:1234/");

            config.MessageHandlers.Add(new CustomHeadersConfig());

            config.Routes.MapHttpRoute(
               name: "default",
               routeTemplate: "{controller}/{action}",
               defaults: new
               {
                   controller = "proxy", 
                   action="proxy"
               }
           );

           HttpSelfHostServer serevr = new HttpSelfHostServer(config);
            
           serevr.OpenAsync().Wait();
           //server started
           
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Form1());
           
        }
    }
}
