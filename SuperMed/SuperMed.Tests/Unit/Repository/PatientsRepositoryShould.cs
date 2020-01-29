using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Entities;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class PatientsRepositoryShould
    {
        [Test]
        [Category("UnitTest")]
        public async Task UpdatePatientsInfo()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;
            
            var patient = fixture.Create<Patient>();

            var firstNameBefore = patient.FirstName;
            var birthDateBefore = patient.BirthDate;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new PatientsRepository(context);
                await repository.CreateAsync(patient, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Patients.Count().Should().Be(1);

                patient.LastName = "TestAfter";
                patient.Phone = "888";

                var repository = new PatientsRepository(context);
                await repository.Update(patient, CancellationToken.None);

                context.Patients.Count().Should().Be(1);
                context.Patients.First().Phone.Should().Be("888");
                context.Patients.First().LastName.Should().Be("TestAfter");
                context.Patients.First().FirstName.Should().Be(firstNameBefore);
                context.Patients.First().BirthDate.Should().Be(birthDateBefore);
            }
        }
    }
}
