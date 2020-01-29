using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SuperMed.Controllers;
using System.Security.Claims;

namespace SuperMed.Tests.Unit.Controller
{
    [TestFixture]
    public class HomeControllerShould
    {
        [Test]
        [Category("UnitTest")]
        public void ReturnPatientsViewWhenUserInPatientRole()
        {
            IFixture fixture = new Fixture();
            var userName = fixture.Create<string>();

            var sut = new HomeController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, userName),
                                    new Claim(ClaimTypes.Role, "Patient") 
                                },
                                "testAuthType"))
                    }
                }
            };

            var result = sut.Index() as RedirectToActionResult;
            result.Should().NotBe(null);
            if (result != null)
            {
                result.ActionName.Should().Be("Index");
                result.ControllerName.Should().Be("Patients");
            }

            sut.Dispose();
        }

        [Test]
        [Category("UnitTest")]
        public void ReturnDoctorsViewWhenUserInDoctorsRole()
        {
            IFixture fixture = new Fixture();
            var userName = fixture.Create<string>();

            var sut = new HomeController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, userName),
                                    new Claim(ClaimTypes.Role, "Doctor")
                                },
                                "testAuthType"))
                    }
                }
            };

            var result = sut.Index() as RedirectToActionResult;
            result.Should().NotBe(null);
            if (result != null)
            {
                result.ActionName.Should().Be("Index");
                result.ControllerName.Should().Be("Doctors");
            }

            sut.Dispose();
        }
    }
}
