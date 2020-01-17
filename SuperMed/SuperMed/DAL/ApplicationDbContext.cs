using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;

namespace SuperMed.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorAbsence> DoctorsAbsences { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(b =>
            {
                b.ToTable("Doctors");
                b.HasKey(x => x.DoctorId);
                b.HasOne(x => x.Specialization);
            });

            builder.Entity<Specialization>(b =>
            {
                b.HasKey(x => x.Id);
            });
        }
    }
}
