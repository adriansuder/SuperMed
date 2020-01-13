using System;
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
            var doctorsViewModel = new DoctorsViewModel
            {
                Appointments = _appointmentsRepository.GetTodaysAppointmentByDoctorName(_userManager.GetUserName(User))
            };
            
            if (doctorsViewModel.Appointments.Count != 0)
            {
                foreach (var appointment in doctorsViewModel.Appointments)
                {
                    appointment.Patient = await _patientsRepository.GetAppointmentByPatientId(appointment.PatientId);
                }
            }

            return View(doctorsViewModel);
        }

        [HttpGet]
        public IActionResult AddDoctorAbsence()
        {
            var addDoctorAbsenceViewModel = new AddDoctorAbsenceViewModel()
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

            var doctor = await _doctorsRepository.GetByName(User.Identity.Name);
            
            var absence = new DoctorAbsence
            {
                AbsenceDate = model.AbsenceDate,
                DoctorId = doctor.DoctorId,
                AbsenceDescription = model.AbsenceDescription
            };

            await _absenceRepository.AddAsync(absence);

            return RedirectToAction("Index", "Home");
        }
    }
}
