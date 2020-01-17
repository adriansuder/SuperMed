using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IPatientsRepository
    {
        Task<Patient> AddPatient(Patient patient);
        Task<Patient> GetPatientById(int patientId);
        Task<Patient> GetPatientByName(string patientName);
        Task<Patient> Update(Patient patient);
    }
}
