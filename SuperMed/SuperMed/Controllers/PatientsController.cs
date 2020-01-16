using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Managers;
using SuperMed.Models.Entities;
using SuperMed.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientsRepository _patientsRepository;
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ISpecializationsRepository _specializationsRepository;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IAbsenceRepository _absenceRepository;

        public PatientsController(
            UserManager<ApplicationUser> userManager, 
            IPatientsRepository patientsRepository,
            IDoctorsRepository doctorsRepository,
            ISpecializationsRepository specializationsRepository,
            IAppointmentsRepository appointmentsRepository,
            IAbsenceRepository absenceRepository)
        {
            _userManager = userManager;
            _patientsRepository = patientsRepository;
            _doctorsRepository = doctorsRepository;
            _specializationsRepository = specializationsRepository;
            _appointmentsRepository = appointmentsRepository;
            _absenceRepository = absenceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var patientViewModel = new PatientViewModel
            {
                Patient = await _patientsRepository.GetPatientByName(User.Identity.Name),
                GetPastAppointments = _appointmentsRepository.GetPastPatientsAppointments(User.Identity.Name),
                GetUpcommingAppointments = _appointmentsRepository.GetUpcommingPatientsAppointments(User.Identity.Name)
            };

            return View(patientViewModel);
        }

        public async Task<IActionResult> CreateVisit()
        {
            var allDoctors = await _doctorsRepository.GetAllDoctors();
            var selectList = new List<SelectListItem>();

            foreach (var doctor in allDoctors)
            {
                var specname = await _specializationsRepository.GetSpecializationById(doctor.SpecializationId);

                selectList.Add(new SelectListItem
                {
                    Value = doctor.Name,
                    Text = $"{specname.Name} - {doctor.FirstName} {doctor.LastName}"
                });
            }

            var orderedDoctors = selectList.OrderBy(m => m.Text).ToList();
            var visitModel = new CreateVisitViewModel
            {
                Doctors = orderedDoctors,
                StartDateTime = DateTime.Today
            };

            return View(visitModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitStep2(CreateVisitViewModel model)
        {
            var isDoctorAvailableOnDate = _absenceRepository.GetDoctorsAbscenceByDate(model.DoctorName, model.StartDateTime);

            if (isDoctorAvailableOnDate != null)
            {
                ModelState.AddModelError("doctorError", "Niestety, wybrany lekarz jest w tym dniu niedostępny");
            }

            if (!ModelState.IsValid)
            {
                var allDoctors = await _doctorsRepository.GetAllDoctors();
                var selectList = new List<SelectListItem>();

                foreach (var doc in allDoctors)
                {
                    var specname = await _specializationsRepository.GetSpecializationById(doc.SpecializationId);
                    selectList.Add(new SelectListItem
                    {
                        Value = doc.Name,
                        Text = $"{specname.Name} - {doc.FirstName} {doc.LastName}"
                    });
                }

                model.Doctors = selectList.OrderBy(m => m.Text).ToList();

                return View("CreateVisit", model);
            }

            var doctor = await _doctorsRepository.GetDoctorByName(model.DoctorName);
            var app = _appointmentsRepository.GetDoctorsAppointmentsByDate(model.StartDateTime, model.DoctorName);

            var dates = AppointmentManager.GetAvailableDates(model.StartDateTime);

            foreach (var date in app)
            {
                dates.RemoveAll(d => d.TimeOfDay == date.StartDateTime.TimeOfDay);
            }
           
            var createVisitStep2ViewModel = new CreateVisitStep2ViewModel
            {
                Date = dates,
                Doctor = doctor,
                DoctorName = model.DoctorName,
                StartDateTime = model.StartDateTime,
                Description = model.Description
            };

            return View(createVisitStep2ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitStep3(CreateVisitStep2ViewModel model)
        {
            var correctTime = AppointmentManager.GenerateDateTimes(model.StartDateTime)
                .Any(d => d.Date.ToString("d") == model.TimeOfDay.ToString("d"));
            
            if (!correctTime)
            {
                ModelState.AddModelError("doctorError", "Niestety, wybrany lekarz jest w tym dniu niedostępny");
            }

            if (!ModelState.IsValid)
            {

            }

            var doctor = await _doctorsRepository.GetDoctorByName(model.DoctorName);
            var createVisitStep3ViewModel = new CreateVisitStep3ViewModel
            {
                StartDateTime = new DateTime(model.StartDateTime.Year, model.StartDateTime.Month,
                    model.StartDateTime.Day, model.TimeOfDay.Hour, model.TimeOfDay.Minute, 0),
                Doctor = doctor,
                DoctorName = model.DoctorName,
                Description = model.Description,
                TimeOfDay = model.TimeOfDay
            };

            return View(createVisitStep3ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVisit(CreateVisitStep3ViewModel model)
        {
            var userName = _userManager.GetUserName(User);
            var doctor = await _doctorsRepository.GetDoctorByName(model.DoctorName);
            var patient = await _patientsRepository.GetPatientByName(userName);

            var appointment = new Appointment
            {
                StartDateTime = new DateTime(model.StartDateTime.Year, model.StartDateTime.Month,
                    model.StartDateTime.Day, model.TimeOfDay.Hour, model.TimeOfDay.Minute, 0),
                Doctor = doctor,
                Patient = patient,
                Status = Status.New,
                Description = model.Description
            };

            await _appointmentsRepository.Add(appointment);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            var userName = _userManager.GetUserName(User);
            var patient = await _patientsRepository.GetPatientByName(userName);

            return View("ChangeInfo", patient);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitChangedInfo(Patient patient)
        {
            var userName = _userManager.GetUserName(User);
            var actualPatient = await _patientsRepository.GetPatientByName(userName);

            actualPatient.LastName = patient.LastName;
            actualPatient.Phone = patient.Phone;

            await _patientsRepository.Update(actualPatient);
            
            return RedirectToAction("Index", "Home");
        }
    }
}