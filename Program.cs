using Microsoft.Owin.Hosting;
using PrintHook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrintHook
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var settingsService = new SettingsService();
            var settings = settingsService.GetSettings();
            var port = settings.Port;
            string baseAddress = $"http://localhost:{port}/";
            using (WebApp.Start(baseAddress))
            {
                Console.WriteLine("Server running on {0}", baseAddress);
                #if DEBUG
                    // While debugging this section is used.
                    var service = new Service();
                    service.OnDebug();
                    System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                #else
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new Service(settings)
                    };
                    ServiceBase.Run(ServicesToRun);
                #endif
            }
        }
    }
}
