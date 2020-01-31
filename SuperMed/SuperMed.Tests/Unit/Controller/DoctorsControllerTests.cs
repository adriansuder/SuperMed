using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SuperMed.Controllers;
using SuperMed.Models.ViewModels;
using SuperMed.Services;

namespace SuperMed.Tests.Unit.Controller
{
    [TestFixture]
    public class DoctorsControllerTests
    {
        private Mock<IAppService> _appService;

        [SetUp]
        public void Setup()
        {
            _appService = new Mock<IAppService>();
        }

        [Test]
        [Category("UnitTest")]
        public async Task Index_Returns_ViewResult()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.Create<DoctorsViewModel>();

            _appService.Setup(c =>
                    c.GetDoctorsAppointmentsForDay(It.IsAny<string>(), It.IsAny<DateTime>(), CancellationToken.None))
                .ReturnsAsync(model);

            var appointmentsController = new DoctorsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") },
                            "test"))
                    }
                }
            };

            var action = await appointmentsController.Index();

            Assert.IsInstanceOf<ViewResult>(action);
        }

        [Test]
        [Category("UnitTest")]
        public async Task AddDoctorAppointment_Returns_ViewResult_WhenModelNotValid()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.Create<AddDoctorAbsenceViewModel>();

            var doctorsController = new DoctorsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") },
                            "test"))
                    }
                }
            };

            doctorsController.ModelState.AddModelError("errorkey", "Error");

            var action = await doctorsController.AddDoctorAbsence(model);

            Assert.IsInstanceOf<ViewResult>(action);
        }

        [Test]
        [Category("UnitTest")]
        public async Task AddDoctorAppointment_Returns_RedirectToAction()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.Create<AddDoctorAbsenceViewModel>();

            var doctorsController = new DoctorsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") },
                            "test"))
                    }
                }
            };

            var action = await doctorsController.AddDoctorAbsence(model);

            Assert.IsInstanceOf<RedirectToActionResult>(action);
        }
    }
}
