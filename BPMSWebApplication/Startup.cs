using BPMSApi;
using BPMSWebApplication.Configs;
using BPMSWebApplication.Model.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BPMSWebApplication
{
    public class Startup
    {
        private IBPMSSettings Settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var instance = new BPMSApiInstance(new BPMSConfig());
            instance.AuthenticationFunctions.WriteBaseJson();
            //services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Loads the settings for the application
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            switch (environment.ToLower())
            {
                case "production":
                    services.AddTransient<IBPMSSettings, ProductionConfig>();
                    Settings = new ProductionConfig();
                    break;
                default:
                    services.AddTransient<IBPMSSettings, DevelopmentConfig>();
                    Settings = new DevelopmentConfig();
                    break;
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
