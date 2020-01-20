using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class PatientsRepositoryShould
    {
        [Test]
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
                await repository.AddPatient(patient);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.Patients.Count().Should().Be(1);

                patient.LastName = "TestAfter";
                patient.Phone = "888";

                var repository = new PatientsRepository(context);
                await repository.Update(patient);

                context.Patients.Count().Should().Be(1);
                context.Patients.FirstOrDefault()?.Phone.Should().Be("888");
                context.Patients.FirstOrDefault()?.LastName.Should().Be("TestAfter");
                context.Patients.FirstOrDefault()?.FirstName.Should().Be(firstNameBefore);
                context.Patients.FirstOrDefault()?.BirthDate.Should().Be(birthDateBefore);
            }
        }
    }
}
