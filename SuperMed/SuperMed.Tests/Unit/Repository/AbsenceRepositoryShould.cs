using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class AbsenceRepositoryShould
    {
        private Mock<DbSet<DoctorAbsence>> mockSet;
        private Mock<ApplicationDbContext> mockContext;
        private AbsenceRepository absenceRepository;
        private DoctorAbsence testDoctorAbsence;

        [SetUp]
        public void Setup()
        {
            IFixture fixture = new Fixture();
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            mockSet = new Mock<DbSet<DoctorAbsence>>();

            mockContext = new Mock<ApplicationDbContext>(options);
            mockContext.Setup(m => m.DoctorsAbsences).Returns(mockSet.Object);
            absenceRepository = new AbsenceRepository(mockContext.Object);
            
            testDoctorAbsence = fixture.Create<DoctorAbsence>();
        }

        [Test]
        public async Task CallAddAsyncAndSaveChangesAsyncOnce()
        {
            await absenceRepository.CreateAsync(testDoctorAbsence, CancellationToken.None);

            mockSet.Verify(m => m.AddAsync(testDoctorAbsence, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldAddWriteToDatabase()
        {
            IFixture fixture = new Fixture();
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            using (var context = new ApplicationDbContext(options))
            {
                await context.DoctorsAbsences.AddAsync(testDoctorAbsence);
                await context.SaveChangesAsync(CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.DoctorsAbsences.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task AddAbsenceToDatabase()
        {
            IFixture fixture = new Fixture();
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AbsenceRepository(context);
                await repository.CreateAsync(testDoctorAbsence, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.DoctorsAbsences.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task ReturnAbsenceFromDatabase()
        {
            IFixture fixture = new Fixture();
            var databaseName = fixture.Create<string>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AbsenceRepository(context);
                await repository.CreateAsync(testDoctorAbsence, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                //var repository = new AbsenceRepository(context);
                //var ret = await repository.GetAsync(testDoctorAbsence.Doctor.Id, testDoctorAbsence.AbsenceDate);

                //ret.AbsenceDate.Should().Be(testDoctorAbsence.AbsenceDate);
                //ret.AbsenceDescription.Should().Be(testDoctorAbsence.AbsenceDescription);
            }
        }
    }
}
