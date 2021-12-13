using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Pharmacy.Service;
using Microsoft.Extensions.DependencyInjection;
using Ionic.Zip;
using System.IO;
using System.Timers;

namespace Integration_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(FilesService.CompressFiles);
            timer.Start();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RabbitMQService>();
                });
    }
}
