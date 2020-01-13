using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using SuperMed.Abstractions.Consts;

namespace SuperMed.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var isPatient = await _userManager.IsInRoleAsync(currentUser, Roles.Patient);
                var isDoctor = await _userManager.IsInRoleAsync(currentUser, Roles.Doctor);

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