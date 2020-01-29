using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SuperMed.Controllers;
using SuperMed.Models.ViewModels;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Services;

namespace SuperMed.Tests.Unit.Controller
{
    [TestFixture]
    public class PatientsControllerShould
    {
        private Mock<IAppService> _appService;

        [SetUp]
        public void Setup()
        {
            _appService = new Mock<IAppService>();
        }

        [Test]
        [Category("UnitTest")]
        public async Task Index_Return_View()
        {
            var sut = new PatientsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tesuser") },
                                "testAuthType"))
                    }
                }
            };

            var result = await sut.Index();
            result.Should().BeOfType<ViewResult>();

            sut.Dispose();
        }

        [Test]
        [Category("UnitTest")]
        public async Task CreateVisitStep2_Returns_View_When_ModelStateInvalid()
        {
            _appService.Setup(m => m.GetDoctorsAppointmentsForDay(It.IsAny<string>(), It.IsAny<DateTime>(), CancellationToken.None))
                .ReturnsAsync(It.IsAny<DoctorsViewModel>());

            var sut = new PatientsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tesuser") },
                                "testAuthType"))
                    }
                }
            };

            sut.ModelState.AddModelError("test", "test");

            var result = await sut.CreateVisitStep2(new CreateVisitViewModel());
            result.Should().BeOfType<ViewResult>();

            sut.Dispose();
        }

        [Test]
        [Category("UnitTest")]
        public async Task SubmitChangedInfo_Redirects_To_IndexAction()
        {
            _appService.Setup(m => m.ChangePatientInfo(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(It.IsAny<ChangePatientInfoViewModel>());
            
            var sut = new PatientsController(_appService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tesuser") },
                                "testAuthType"))
                    }
                }
            };

            var result = await sut.SubmitChangedInfo(new ChangePatientInfoViewModel(), CancellationToken.None);
            result.Should().BeOfType<RedirectToActionResult>();

            sut.Dispose();
        }
    }
}
