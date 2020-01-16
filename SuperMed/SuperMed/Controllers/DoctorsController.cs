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
            var user = await _doctorsRepository.GetDoctorByName(User.Identity.Name);
            var doctorsViewModel = new DoctorsViewModel
            {
                Appointments = _appointmentsRepository.GetTodaysAppointmentByDoctorName(user.Name),
                NextDoctorsAbsences = _absenceRepository.GetNextDoctorAbsences(user.DoctorId)
            };
            
            if (doctorsViewModel.NextDoctorsAbsences.Count != 0)
            {
                foreach (var appointment in doctorsViewModel.Appointments)
                {
                    appointment.Patient = await _patientsRepository.GetAppointmentByPatientId(appointment.PatientId);
                }
            }

            return View(doctorsViewModel);
        }

        public async Task<IActionResult> DoctorAppoinmentHistory()
        {
            var doctor = await _doctorsRepository.GetDoctorByName(User.Identity.Name);
            var doctorAppointmentHistoryViewModel = new DoctorAppointmentHistoryViewModel
            {
                RealizedAppointments = _appointmentsRepository.GetDoctorsRealizedAppoinmentById(DateTime.Now,doctor.DoctorId)
            };

            if (doctorAppointmentHistoryViewModel.RealizedAppointments.Count != 0)
            {
                foreach (var appointment in doctorAppointmentHistoryViewModel.RealizedAppointments)
                {
                    appointment.Patient = await _patientsRepository.GetAppointmentByPatientId(appointment.PatientId);
                }
            }
            return View(doctorAppointmentHistoryViewModel);
        }

        public async Task<IActionResult> EditDoctorAbsences()
        {
            var doctor = await _doctorsRepository.GetDoctorByName(User.Identity.Name);
            var editDoctorAbsence = new EditDoctorAbsencesViewModel
            {
                DoctorAbsences = _absenceRepository.GetDoctorAbsencesToEdit(doctor.DoctorId)
            };

            return View(editDoctorAbsence);
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

            var doctor = await _doctorsRepository.GetDoctorByName(User.Identity.Name);
            
            var absence = new DoctorAbsence
            {
                AbsenceDate = model.AbsenceDate,
                DoctorId = doctor.DoctorId,
                AbsenceDescription = model.AbsenceDescription
            };

            await _absenceRepository.AddAsync(absence);

            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        public async Task<IActionResult> DeleteDoctorAbsence(int Id, EditDoctorAbsencesViewModel model) //int Id, EditDoctorAbsencesViewModel model
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _absenceRepository.DeleteAbsence(Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
