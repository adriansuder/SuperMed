using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Models.ViewModels;
using SuperMed.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientsController : Controller
    {
        private readonly IAppService _appService;

        public PatientsController(IAppService appService)
        {
            _appService = appService;
        }

        public async Task<IActionResult> Index()
        {
            var patientViewModel =
                await _appService.GetPatientsAppointments(User.Identity.Name, CancellationToken.None);

            return View(patientViewModel);
        }

        public async Task<IActionResult> CreateVisit()
        {
            var createVisitViewModel = await _appService.CreateVisit(CancellationToken.None);
            
            return View(createVisitViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitStep2(CreateVisitViewModel model)
        {
            bool isDoctorAvailableOnDate = await _appService.GetHasDoctorAbsenceOnDate(model.DoctorName, model.StartDateTime, CancellationToken.None);
            
            if (isDoctorAvailableOnDate)
            {
                ModelState.AddModelError("doctorError", "Niestety, wybrany lekarz jest w tym dniu niedostępny");
            }

            if (!ModelState.IsValid)
            {
                var createVisitViewModel = await _appService.CreateVisit(CancellationToken.None);

                return View("CreateVisit", createVisitViewModel);
            }
            
            var createVisitStep2ViewModel = await _appService.CreateVisitStepTwo(model, CancellationToken.None);
            
            return View(createVisitStep2ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitStep3(CreateVisitStep2ViewModel model)
        {
            CreateVisitStep3ViewModel createVisitStep3ViewModel = await _appService.CreateVisitStepThree(model, CancellationToken.None);

            return View(createVisitStep3ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVisit(CreateVisitStep3ViewModel model)
        {
            try
            {
                await _appService.SubmitVisit(User.Identity.Name, model, CancellationToken.None);
            }
            catch
            {
                return View("CreateVisitError");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            var changePatientInfoViewModel =
                await _appService.ChangePatientInfo(User.Identity.Name, CancellationToken.None);

            return View("ChangeInfo", changePatientInfoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitChangedInfo(ChangePatientInfoViewModel changePatientInfoViewModel, CancellationToken cancellationToken)
        {
            await _appService.SaveChangedPatientInfo(User.Identity.Name, changePatientInfoViewModel, cancellationToken);
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PatientAppointmentHistory()
        {
            var patientAppointmentHistoryViewModel =
                await _appService.GetPatientsRealizedAppointments(User.Identity.Name, CancellationToken.None);
            
            return View(patientAppointmentHistoryViewModel);
        }
    }
}