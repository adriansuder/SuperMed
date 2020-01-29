using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class AppointmentsRepositoryShould
    {
        [Test]
        public async Task AddAppointmentToDatabase()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AppointmentsRepository(context);
                await repository.AddAppointment(fixture.Create<Appointment>());
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Appointments.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task DeleteAppointmentFromDatabase()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var appointment = fixture.Create<Appointment>();

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
