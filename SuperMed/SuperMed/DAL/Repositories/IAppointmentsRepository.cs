using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<Appointment> Add(Appointment appointment);
        Task<List<Appointment>> GetByDoctorName(string docName);
        Task<List<Appointment>> GetByPatientName(string docName);
    }
}
