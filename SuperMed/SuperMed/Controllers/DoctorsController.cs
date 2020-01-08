using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using SuperMed.Models.ViewModels;


namespace SuperMed.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IPatientsRepository _patientsRepository;
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IDoctorsRepository _doctorsRepository;

        public DoctorsController(
            UserManager<ApplicationUser> userManager,
            IAppointmentsRepository appointmentsRepository,
            IPatientsRepository patientsRepository, IAbsenceRepository absenceRepository, IDoctorsRepository doctorsRepository)
        {
            _userManager = userManager;
            _appointmentsRepository = appointmentsRepository;
            _patientsRepository = patientsRepository;
            _absenceRepository = absenceRepository;
            _doctorsRepository = doctorsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var docName = _userManager.GetUserName(User);

            var docViewModel = new DoctorsViewModel
            {
                Appointments = await _appointmentsRepository.GetByDoctorName(docName)
            };


            if (docViewModel.Appointments.Count != 0)
            {
                foreach (var appointment in docViewModel.Appointments)
                {
                    appointment.Patient = await _patientsRepository.Get(appointment.PatientId);
                }
            }

            return View(docViewModel);
        }

        public async Task<IActionResult> AddDoctorAbsence()
        {
            //var docId = _userManager.GetUserId(User);
            var absenceModel = new AddDoctorAbsenceViewModel
            {
               
            };

            return View(absenceModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAbsence(AddDoctorAbsenceViewModel model)
        {
            var userName = _userManager.GetUserName(User);
            var doctor = await _doctorsRepository.GetByName(userName);
            var absence = new DoctorAbsence
            {
                AbsenceDate = model.AbsenceDate,
                DoctorId =doctor.DoctorId,
                AbsenceDescription = model.AbsenceDescription
            };

            await _absenceRepository.Add(absence);

            return RedirectToAction("Index", "Home");
        }
    }
}
