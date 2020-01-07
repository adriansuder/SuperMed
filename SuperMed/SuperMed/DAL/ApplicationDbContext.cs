using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;

namespace SuperMed.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<DoctorAbsence> DoctorsAbsences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(b =>
            {
                b.ToTable("Doctors");
                b.HasKey(x => x.DoctorId);
                b.HasOne(x => x.Specialization);
            });



            builder.Entity<Specialization>(b => { b.HasKey(x => x.Id); });
        }
    }
}
