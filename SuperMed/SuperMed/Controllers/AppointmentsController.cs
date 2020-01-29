using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using SuperMed.Models.ViewModels;
using System.Threading.Tasks;

namespace SuperMed.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IDoctorsRepository _doctorsRepository;

        public AppointmentsController(
            IAppointmentsRepository appointmentsRepository,
            IDoctorsRepository doctorsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
            _doctorsRepository = doctorsRepository;
        }

        [HttpGet("{id}")]
        [Route("Appointment")]
        public async Task<IActionResult> Index(int id)
        {
            var appointment = await _appointmentsRepository.GetAppointmentById(id);
            var isDoctor = await _doctorsRepository.GetDoctorByName(User.Identity.Name) != null;

            if (appointment == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (appointment.Patient.Name != User.Identity.Name && appointment.Doctor.Name != User.Identity.Name)
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

        [HttpGet("{id}")]
        [Route("DeleteAppointment")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointmentToRemove = await _appointmentsRepository.GetAppointmentById(id);

            if(appointmentToRemove.Patient.Name == User.Identity.Name)
            {
                await _appointmentsRepository.DeleteAppointmentById(id);
            }

            return RedirectToAction("Index", "Patients");
        }

        [HttpPost("{id}")]
        [Route("Save")]
        public async Task<IActionResult> Save(EditAppointmentViewModel model)
        {
            var appointment = await _appointmentsRepository.GetAppointmentById(model.Appointment.Id);

            appointment.Review = model.Appointment.Review;
            appointment.Status = Status.Finished;

            await _appointmentsRepository.FinishAppointment(appointment);

            return RedirectToAction("Index", "Doctors");
        }
    }
}