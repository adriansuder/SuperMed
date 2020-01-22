using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Entities;
using SuperMed.Models.ViewModels;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Services;

namespace SuperMed.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppService _appService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAppService appService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appService = appService;
        }

        public IActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }

        public IActionResult RegisterPatient()
        {
            return View("RegisterPatient");
        }

        public IActionResult RegisterDoctor()
        {
            return View("RegisterDoctor");
        }

        [HttpPost]
        public IActionResult RegisterAdmin(RegisterAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View("RegisterSuccessful");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient(RegisterPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Name,
                IsActive = true
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);

            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Patient");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, model.Name));

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    var patient = new Patient
                    {
                        Name = model.Name,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        Phone = model.Phone,
                        ApplicationUser = user
                    };

                    await _appService.AddPatient(patient, CancellationToken.None);

                    return View("RegisterSuccessful");
                }
            }

            foreach (var identityError in createUserResult.Errors)
            {
                if (identityError.Code == "DuplicateUserName")
                {
                    ModelState.AddModelError("", "Ta nazwa użytkownika jest już zajęta.");
                }
                else
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDoctor(RegisterDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Name,
                IsActive = true
            };
            
            var createUserResult = await _userManager.CreateAsync(user, model.Password);

            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, model.Name));

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    await _appService.AddSpecialization(
                        new Specialization
                        {
                            Name = model.Specialization
                        }, CancellationToken.None);

                    var docsSpec = await _appService.GetSpecializationByName(model.Specialization, CancellationToken.None);

                    var doctor = new Doctor
                    {
                        Name = model.Name,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone,
                        Specialization = docsSpec,
                        ApplicationUser = user
                    };

                    await _appService.AddDoctor(doctor, CancellationToken.None);
                    
                    return View("RegisterSuccessful");
                }
            }

            foreach (var identityError in createUserResult.Errors)
            {
                if (identityError.Code == "DuplicateUserName")
                {
                    ModelState.AddModelError("", "Ta nazwa użytkownika jest już zajęta.");
                }
                else
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.Name, model.Password, true, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Niepoprawne dane logowania.");

            return View(model);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

