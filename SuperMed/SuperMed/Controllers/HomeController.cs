using Microsoft.AspNetCore.Mvc;
using SuperMed.Models.ViewModels;
using System.Diagnostics;

namespace SuperMed.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var isPatient = User.IsInRole("Patient");
                var isDoctor = User.IsInRole("Doctor");

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