using Microsoft.Extensions.DependencyInjection;
using SuperMed.DAL.Repositories;
using SuperMed.Entities;
using SuperMed.Services;

namespace SuperMed.DI
{
    public static class ApplicationModules
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<DoctorAbsence>, AbsenceRepository>();
            services.AddScoped<IRepository<Doctor>, DoctorsRepository>();
            services.AddScoped<IRepository<Specialization>, SpecializationsRepository>();
            services.AddScoped<IRepository<Appointment>, AppointmentsRepository>();
            services.AddScoped<IRepository<Patient>, PatientsRepository>();
            services.AddScoped<IAppService, AppService>();
        }
    }
}
