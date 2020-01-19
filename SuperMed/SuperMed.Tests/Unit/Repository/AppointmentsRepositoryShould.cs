using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class AppointmentsRepositoryShould
    {
        [Test]
        public async Task AddAppointmentToDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "appointmentTest1").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AppointmentsRepository(context);
                await repository.AddAppointment(new Appointment
                {
                    Description = "test appointment",
                    StartDateTime = DateTime.Now,
                    Status = Status.New
                });
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Appointments.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task DeleteAppointmentFromDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "appointmentTest2").Options;

            var appointment = new Appointment
            {
                Id = 1,
                Description = "test appointment",
                StartDateTime = DateTime.Now,
                Status = Status.New,
                Patient = new Patient { Name = "TestPatient "},
                Doctor = new Doctor { Name = "Test Doctor"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AppointmentsRepository(context);
                await repository.AddAppointment(appointment);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Appointments.Count().Should().Be(1);

                var repository = new AppointmentsRepository(context);
                await repository.DeleteAppointmentById(appointment.Id);
                
                context.Appointments.Count().Should().Be(0);
            }
        }
    }
}
