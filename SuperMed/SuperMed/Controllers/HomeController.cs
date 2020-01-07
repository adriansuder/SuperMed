using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SuperMed.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPatientsRepository _patientsRepository;
        private readonly IDoctorsRepository _doctorsRepository;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPatientsRepository patientsRepository,
            IDoctorsRepository doctorsRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _patientsRepository = patientsRepository;
            _doctorsRepository = doctorsRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var isPatient = await _userManager.IsInRoleAsync(currentUser, "Patient");
                var isDoctor = await _userManager.IsInRoleAsync(currentUser, "Doctor");

                if (isDoctor)
                {
                    return RedirectToAction("Index", "Doctors");
                }

                if (isPatient)
                {
                    return RedirectToAction("Index", "Patients");
                }

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}