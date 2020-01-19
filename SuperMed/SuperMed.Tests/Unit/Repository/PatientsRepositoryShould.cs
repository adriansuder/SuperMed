using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class PatientsRepositoryShould
    {
        [Test]
        public async Task UpdatePatientsInfo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "patientsRepositoryTest1").Options;
            
            var patient = new Patient
            {
                PatientId = 1,
                FirstName = "TestFirstName",
                LastName = "TestBefore",
                BirthDate = DateTime.Parse("1990/05/02"),
                Phone = "333"
            };

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
                context.Patients.FirstOrDefault()?.FirstName.Should().Be("TestFirstName");
                context.Patients.FirstOrDefault()?.BirthDate.Should().Be(DateTime.Parse("1990/05/02"));
            }
        }
    }
}
