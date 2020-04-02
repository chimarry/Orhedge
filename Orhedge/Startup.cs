using System;
using System.Collections.Generic;
using System.Globalization;
using DatabaseLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orhedge.IoC;

namespace Orhedge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<OrhedgeContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.Cookie.IsEssential = true;
                    opts.LoginPath = "/Home/Login";
                    opts.SlidingExpiration = true;
                    opts.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization( 
                opts =>
                opts.DataAnnotationLocalizerProvider = 
                (type, factory) => factory.Create(typeof(SharedResource)));

            return DependencyInjectionConfiguration.Configure(services);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("sr")
            };

            app.UseRequestLocalization(opts =>
            {
                opts.DefaultRequestCulture = new RequestCulture("sr", "sr");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
