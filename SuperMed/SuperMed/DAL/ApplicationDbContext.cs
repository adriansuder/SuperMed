using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperMed.Auth;
using SuperMed.Entities;

namespace SuperMed.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorAbsence> DoctorsAbsences { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SuperMedDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(b =>
            {
                b.ToTable("Doctors");
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Specialization);
                b.HasOne(x => x.ApplicationUser);
            });

            builder.Entity<Patient>(b => { b.HasOne(x => x.ApplicationUser); });

            builder.Entity<Specialization>(b =>
            {
                b.HasKey(x => x.SpecializationId);
            });
        }
    }
}
