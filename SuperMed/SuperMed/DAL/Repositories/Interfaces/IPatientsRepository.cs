using System.Threading;
using System.Threading.Tasks;
using SuperMed.Entities;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IPatientsRepository
    {
        Task<Patient> AddPatient(Patient patient, CancellationToken cancellationToken);
        Task<Patient> GetPatientById(int patientId);
        Task<Patient> GetPatientByName(string patientName);
        Task<Patient> Update(Patient patient);
    }
}
