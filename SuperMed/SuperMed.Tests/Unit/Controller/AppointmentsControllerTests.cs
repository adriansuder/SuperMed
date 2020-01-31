using NUnit.Framework;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SuperMed.Controllers;
using SuperMed.Models.ViewModels;
using SuperMed.Services;

namespace SuperMed.Tests.Unit.Controller
{
    [TestFixture]
    public class AppointmentsControllerTests
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
            var model = fixture.Create<EditAppointmentViewModel>();

            _appService.Setup(c => c.EditAppointment(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(model);

            var appointmentsController = new AppointmentsController(_appService.Object)
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

            var action = await appointmentsController.Index(1);

            Assert.IsInstanceOf<ViewResult>(action);
        }

        [Test]
        [Category("UnitTest")]
        public async Task Index_Returns_RedirectToActionResult_When_ModelEmpty()
        {
            var appointmentsController = new AppointmentsController(_appService.Object)
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

            var action = await appointmentsController.Index(1);

            Assert.IsInstanceOf<RedirectToActionResult>(action);
        }


        [Test]
        [Category("UnitTest")]
        public async Task Delete_Returns_RedirectToActionResult()
        {
            var appointmentsController = new AppointmentsController(_appService.Object)
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

            var action = await appointmentsController.Delete(1);

            Assert.IsInstanceOf<RedirectToActionResult>(action);
        }

        [Test]
        [Category("UnitTest")]
        public async Task Save_Returns_RedirectToActionResult()
        {
            IFixture fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = fixture.Create<EditAppointmentViewModel>();

            var appointmentsController = new AppointmentsController(_appService.Object)
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

            var action = await appointmentsController.Save(model);

            Assert.IsInstanceOf<RedirectToActionResult>(action);
        }
    }
}
