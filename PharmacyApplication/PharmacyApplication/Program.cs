using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*MedicineRepository mr = new MedicineRepository();
            Medicine medicine = new Medicine(151564545, "Brufen", "Galenika", "Headache", "Drinking", null, 210, "Overuse", "Death", "Water", 5);
            Medicine medicine2 = new Medicine(1515645451, "Andol", "Galenika", "Headache", "Drinking", null, 210, "Overuse", "Death", "Water", 5);
            //mr.AddMedicine(medicine);
            //mr.AddMedicine(medicine2);

            mr.UpdateMedicine(new Medicine(151564545, "Paracetamol", "Galenika", "Headache", "Drinking", null, 200, "Overuse", "Death", "Water", 5));
            //mr.DeleteMedicine(medicine2.MedicineId);*/
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
