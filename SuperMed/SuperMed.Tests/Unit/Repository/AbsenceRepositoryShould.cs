using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SuperMed.DAL;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;

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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName:"test").Options;

            mockSet = new Mock<DbSet<DoctorAbsence>>();

            mockContext = new Mock<ApplicationDbContext>(options);
            mockContext.Setup(m => m.DoctorsAbsences).Returns(mockSet.Object);
            absenceRepository = new AbsenceRepository(mockContext.Object);
            
            testDoctorAbsence = new DoctorAbsence
            {
                Doctor = new Doctor { Name = "test" },
                AbsenceDate = DateTime.Now,
                AbsenceDescription = "test description"
            };
        }

        [Test]
        public async Task AddAbsence()
        {
            await absenceRepository.AddAbsence(testDoctorAbsence);

            mockSet.Verify(m => m.AddAsync(testDoctorAbsence, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldAddWriteToDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test1").Options;

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
        public async Task RepoShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test2").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AbsenceRepository(context);
                await repository.AddAbsence(testDoctorAbsence);
            }

            using (var context = new ApplicationDbContext(options))
            {
                context.DoctorsAbsences.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task RepoShouldReturn()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test3").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AbsenceRepository(context);
                await repository.AddAbsence(testDoctorAbsence);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new AbsenceRepository(context);
                var ret = await repository.GetDoctorsAbscenceByDate(testDoctorAbsence.Doctor.Name, testDoctorAbsence.AbsenceDate);

                ret.AbsenceDate.Should().Be(testDoctorAbsence.AbsenceDate);
                ret.AbsenceDescription.Should().Be(testDoctorAbsence.AbsenceDescription);
            }
        }
    }
}
