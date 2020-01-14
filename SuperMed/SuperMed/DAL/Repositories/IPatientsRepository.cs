using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IPatientsRepository
    {
        Task<Patient> GetAppointmentByPatientId(int patientId);
        Task<Patient> GetPatientByName(string name);
        Task<Patient> Add(Patient patient);
        Task<Patient> Update(Patient patient);
    }
}
