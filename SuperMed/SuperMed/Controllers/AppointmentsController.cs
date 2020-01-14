using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models.ViewModels;

namespace SuperMed.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IDoctorsRepository _doctorsRepository;

        public AppointmentsController(
            UserManager<ApplicationUser> userManager, 
            IAppointmentsRepository appointmentsRepository,
            IDoctorsRepository doctorsRepository)
        {
            _userManager = userManager;
            _appointmentsRepository = appointmentsRepository;
            _doctorsRepository = doctorsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var appointment = await _appointmentsRepository.GetAppointmentById(id);
            var currentUser = _userManager.GetUserName(User);
            var isDoctor = await _doctorsRepository.GetDoctorByName(currentUser) != null;

            if (appointment.Patient.Name != currentUser && appointment.Doctor.Name != currentUser)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var edit = new EditAppointmentViewModel
            {
                Appointment = appointment,
                IsDoctor = isDoctor
            };

            return View(edit);
        }
    }
}