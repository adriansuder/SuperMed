using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMed.Models.ViewModels;
using System.Threading.Tasks;
using SuperMed.Consts;
using SuperMed.Services;

namespace SuperMed.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppService _appService;

        public AppointmentsController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet(nameof(ControllerRoutes.Id))]
        [Route(nameof(ControllerRoutes.Appointment))]
        public async Task<IActionResult> Index(int id)
        {
            var model = await _appService.EditAppointment(id, User.Identity.Name, CancellationToken.None);
                
            if (model == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View(model);
        }

        [HttpGet(nameof(ControllerRoutes.Id))]
        [Route(nameof(ControllerRoutes.DeleteAppointment))]
        public async Task<IActionResult> Delete(int id)
        {
            await _appService.DeleteAppointmentById(id, User.Identity.Name, CancellationToken.None);

            return RedirectToAction("Index", "Patients");
        }

        [HttpPost(nameof(ControllerRoutes.Id))]
        [Route(nameof(ControllerRoutes.Save))]
        public async Task<IActionResult> Save(EditAppointmentViewModel model)
        {
            await _appService.EditAppointment(model.Appointment.Id, model, CancellationToken.None);
            
            return RedirectToAction("Index", "Doctors");
        }
    }
}