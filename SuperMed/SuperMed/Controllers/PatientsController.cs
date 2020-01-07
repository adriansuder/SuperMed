using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models.Entities;
using SuperMed.Models.ViewModels;

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

        public PatientsController(
            UserManager<ApplicationUser> userManager, 
            IPatientsRepository patientsRepository,
            IDoctorsRepository doctorsRepository,
            ISpecializationsRepository specializationsRepository,
            IAppointmentsRepository appointmentsRepository)
        {
            _userManager = userManager;
            _patientsRepository = patientsRepository;
            _doctorsRepository = doctorsRepository;
            _specializationsRepository = specializationsRepository;
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserName(User);
            var patientInfo = new Patient
            {
                Appointments = await _appointmentsRepository.GetByPatientName(user),
            };

            if (patientInfo.Appointments.Count != 0)
            {
                foreach (var appointment in patientInfo.Appointments)
                {
                    appointment.Doctor = await _doctorsRepository.Get(appointment.DoctorId);
                }
            }
            var getUserInfo = await _patientsRepository.GetByName(user);
            return View(getUserInfo);
        }

        public async Task<IActionResult> CreateVisit()
        {
            var allDoctors = await _doctorsRepository.GetAll();

            var selectList = new List<SelectListItem>();

            foreach (var doctor in allDoctors)
            {
                var specname = await _specializationsRepository.Get(doctor.SpecializationId);
                selectList.Add(new SelectListItem
                {
                    Value = doctor.Name,
                    Text = $"{specname.Name} - {doctor.FirstName} {doctor.LastName}"
                });
            }

            var visitModel = new CreateVisitViewModel
            {
                Doctors = selectList.OrderBy(p => p.Text)
            };

            return View(visitModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitVisit(CreateVisitViewModel model)
        {
            var userName = _userManager.GetUserName(User);
            var doctor = await _doctorsRepository.GetByName(model.Doctor);
            var patient = await _patientsRepository.GetByName(userName);

            var appointment = new Appointment
            {
                StartDateTime = model.StartDateTime,
                Doctor = doctor,
                Patient = patient,
                Status = Status.New,
                Description = model.Description
            };

            await _appointmentsRepository.Add(appointment);

            return RedirectToAction("Index", "Home");
        }
    }
}