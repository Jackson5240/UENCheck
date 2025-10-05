using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UENValidateProj
{
    public class Program
    {
        // ------------------------------------------------------------
        // Entry point of the UENValidateProj web application.
        // This is where the ASP.NET Core runtime begins execution.
        // ------------------------------------------------------------
        public static void Main(string[] args) =>
        // Build and start the web host using the configuration below
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)  // Load default host settings (appsettings.json, environment vars, etc.)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specify the Startup class to define services and middleware.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
