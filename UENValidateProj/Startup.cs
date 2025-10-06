using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UENValidateProj
{
    // ------------------------------------------------------------
    // The Startup class configures the application's services
    // and defines the middleware pipeline for handling HTTP requests.
    // It’s called automatically by the ASP.NET Core runtime.
    // ------------------------------------------------------------
    public class Startup
    {
        // The constructor receives configuration settings (from appsettings.json, env vars, etc.)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Exposes the app’s configuration for use elsewhere if needed.
        public IConfiguration Configuration { get; }

        // ------------------------------------------------------------
        // ConfigureServices() is called by the runtime at startup.
        // This is where you register services for dependency injection (DI),
        // such as controllers, Razor views, database contexts, and custom logic.
        // ------------------------------------------------------------
        public void ConfigureServices(IServiceCollection services)
        {
        // Add MVC controllers and Razor view support.
        // This enables controller classes to return View() results.
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Show detailed error pages for easier debugging during development.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // For now, this is not handled
                // app.UseExceptionHandler("/Home/Error"); // Redirects to a custom error page.
                // app.UseHsts(); // Adds Strict-Transport-Security header to force HTTPS.
            }

            // Enable routing
            app.UseRouting();

            // Define endpoint routing for MVC controllers.
            app.UseEndpoints(endpoints =>
            {
                // Default route:
                // When no controller/action is specified, go to UENInputController.Index().
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UENInput}/{action=Index}/{id?}");
            });
        }
    }
}
