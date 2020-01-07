using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Auth;
using SuperMed.DAL.Repositories;
using SuperMed.Models.ViewModels;

namespace SuperMed.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IPatientsRepository _patientsRepository;

        public DoctorsController(
            UserManager<ApplicationUser> userManager,
            IAppointmentsRepository appointmentsRepository,
            IPatientsRepository patientsRepository)
        {
            _userManager = userManager;
            _appointmentsRepository = appointmentsRepository;
            _patientsRepository = patientsRepository;
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
    }
}
