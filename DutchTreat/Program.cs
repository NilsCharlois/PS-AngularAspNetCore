using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DutchTreat.Data;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            SeedDb(host);

            host.Run();
        }

        private static void SeedDb(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }

        private static void SetupConfiguration(HostBuilderContext context, IConfigurationBuilder builder){
            // remove the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true);
        }    
    }
}
