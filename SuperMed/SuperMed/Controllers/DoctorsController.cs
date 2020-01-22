using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Models.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Consts;
using SuperMed.Services;

namespace SuperMed.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly IAppService _appService;

        public DoctorsController(IAppService appService)
        {
            _appService = appService;
        }

        public async Task<IActionResult> Index()
        {
            var doctorsViewModel = await _appService.GetDoctorsAppointmentsForDay(User.Identity.Name, DateTime.Today, CancellationToken.None);

            return View(doctorsViewModel);
        }

        public async Task<IActionResult> DoctorAppointmentHistory()
        {
            var doctorAppointmentHistoryViewModel = await _appService.GetDoctorsRealizedAppointments(User.Identity.Name, CancellationToken.None);

            return View(doctorAppointmentHistoryViewModel);
        }

        public async Task<IActionResult> EditDoctorAbsences()
        {
            var editDoctorAbsencesViewModel = await _appService.GetDoctorsAbsencesToEdit(User.Identity.Name, CancellationToken.None);
            
            return View(editDoctorAbsencesViewModel);
        }

        [HttpGet]
        public IActionResult AddDoctorAbsence()
        {
            var addDoctorAbsenceViewModel = new AddDoctorAbsenceViewModel
            {
                AbsenceDate = DateTime.Today
            };

            return View(addDoctorAbsenceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorAbsence(AddDoctorAbsenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _appService.AddDoctorsAbsence(User.Identity.Name, model.AbsenceDate, model.AbsenceDescription, CancellationToken.None);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet(nameof(ControllerRoutes.Id))]
        [Route(nameof(ControllerRoutes.DeleteAbscence))]
        public async Task<IActionResult> DeleteDoctorAbsence(int id)
        {
            await _appService.DeleteAbsenceById(id, CancellationToken.None);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
