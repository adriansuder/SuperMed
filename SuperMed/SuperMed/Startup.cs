using System;
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
using SuperMed.DAL.Repositories;

namespace SuperMed
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPatientsRepository, PatientsRepository>();
            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
            services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<IAbsenceRepository, AbsenceRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
