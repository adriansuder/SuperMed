using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMed.Auth;
using SuperMed.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SuperMed.Extensions;

namespace SuperMed
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddRepositories();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "Wartość jest wymagana.");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pl-PL"),
                SupportedCultures = new List<CultureInfo> {new CultureInfo("pl-PL")},
                SupportedUICultures = new List<CultureInfo> {new CultureInfo("pl-PL")}
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var hasPatientRole = roleManager.RoleExistsAsync("Patient");
            hasPatientRole.Wait();

            if (!hasPatientRole.Result)
            {
                var createRoleResult = roleManager.CreateAsync(new IdentityRole("Patient"));
                createRoleResult.Wait();
            }

            var hasDoctorRole = roleManager.RoleExistsAsync("Doctor");
            hasDoctorRole.Wait();

            if (!hasDoctorRole.Result)
            {
                var createRoleResult = roleManager.CreateAsync(new IdentityRole("Doctor"));
                createRoleResult.Wait();
            }
        }
    }
}
