using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Entities;

namespace SuperMed.Tests.Unit.Repository
{
    [TestFixture]
    public class SpecializationsRepositoryTests
    {
        [Test]
        [Category("UnitTest")]
        public async Task Add_and_Return_Specialization()
        {
            IFixture fixture = new Fixture();
            var databaseName = fixture.Create<string>();
            var testSpecialization = fixture.Create<Specialization>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new SpecializationsRepository(context);
                await repository.CreateAsync(testSpecialization, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new SpecializationsRepository(context);
                var actualSpecialization = await repository.GetAsync(testSpecialization.SpecializationId, CancellationToken.None);

                Assert.That(actualSpecialization.SpecializationId, Is.EqualTo(testSpecialization.SpecializationId));
                Assert.That(actualSpecialization.Name, Is.EqualTo(testSpecialization.Name));
            }
        }
    }
}