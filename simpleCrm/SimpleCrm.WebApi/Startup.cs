using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCrm.SqlDbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using SimpleCrm.WebApi.Auth;

namespace SimpleCrm.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var googleOptions = Configuration.GetSection(nameof(GoogleAuthSettings));
            services.Configure<GoogleAuthSettings>(options =>
            {
                options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
                options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
            });

            var microsoftOptions = Configuration.GetSection(nameof(MicrosoftAuthSettings));
            services.Configure<MicrosoftAuthSettings>(options =>
            {
                options.ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                options.ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientSecret)];
            });

            services.AddDbContext<SimpleCrmDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SimpleCrmConnection")));

            services.AddDbContext<CrmIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SimpleCrmConnection")));

            services.AddAuthentication()
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddGoogle(options =>  
                    {
                        options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
                        options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
                    })
                .AddMicrosoftAccount(options =>
                    {
                        options.ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                        options.ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientSecret)];
                    });

            services.AddDefaultIdentity<CrmUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<CrmIdentityDbContext>();
            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = Configuration["SpaRoot"];
            });

            services.AddScoped<ICustomerData, SqlCustomerData>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // add the next one at the END of the pipeline
            app.UseWhen(
              context => !context.Request.Path.StartsWithSegments("/api"),
              appBuilder => appBuilder.UseSpa(spa =>
              {
                  if (env.IsDevelopment())
                  {
                      spa.Options.SourcePath = "../simple-crm-cli";
                      spa.Options.StartupTimeout = new TimeSpan(0, 0, 300); //300 seconds
                  spa.UseAngularCliServer(npmScript: "start");
                  }
              }));
        }
    }
}
