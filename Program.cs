using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using lmsextreg.Data;

namespace lmsextreg
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     BuildWebHost(args).Run();
        // }

        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                }
            }

            host.Run();
        }



        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
