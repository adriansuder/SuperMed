using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using SuperMed.Models.ViewModels;

namespace SuperMed.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPatientsRepository _patientsRepository;
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ISpecializationsRepository _specializationsRepository;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPatientsRepository patientsRepository,
            IDoctorsRepository doctorsRepository,
            ISpecializationsRepository specializationsRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _patientsRepository = patientsRepository;
            _doctorsRepository = doctorsRepository;
            _specializationsRepository = specializationsRepository;
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

            ViewData["AsWho"] = "Admin";
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
                        ApplicationUserID = user.Id
                    };

                    await _patientsRepository.Add(patient);

                    ViewData["AsWho"] = $"Patient" + _userManager.GetUserId(User);
                    return View("RegisterSuccessful");
                }
            }

            foreach (var identityError in createUserResult.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
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
                    await _specializationsRepository.AddSpecialization(
                        new Specialization
                        {
                            Name = model.Specialization
                        });

                    var docsSpec = await _specializationsRepository.GetSpecializationByUserName(model.Specialization);

                    var doctor = new Doctor
                    {
                        Name = model.Name,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Phone = model.Phone,
                        Specialization = docsSpec,
                        ApplicationUserID = user.Id
                    };

                    await _doctorsRepository.Add(doctor);

                    ViewData["AsWho"] = $"Doctor" + _userManager.GetUserId(User);
                    return View("RegisterSuccessful");
                }
            }

            foreach (var identityError in createUserResult.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }

            return View(model);
        }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.Name, model.Password, true, false);

            if (signInResult.Succeeded)
            {
                ViewData["Status"] = $"Zalogowano";
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

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}

