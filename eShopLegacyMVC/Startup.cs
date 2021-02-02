using eShopLegacyMVC.Models;
using eShopLegacyMVC.Models.Infrastructure;
using eShopLegacyMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShopLegacyMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<CatalogDBContext>();
            context.Database.Migrate();
            new CatalogDBInitializer(env, scope.ServiceProvider.GetService<DataSettings>()).Seed(context);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
            services.AddMvc();

            var dataSettings = new DataSettings();
            Configuration.GetSection("DataSettings").Bind(dataSettings);

            services.AddSingleton(dataSettings);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CatalogDBContext>(opt =>
                opt.UseSqlServer(connectionString));

            if (dataSettings.UseMockData)
            {
                services.AddTransient<ICatalogService, CatalogServiceMock>();
            }
            else
            {
                services.AddTransient<ICatalogService, CatalogService>();
            }
        }
    }
}
