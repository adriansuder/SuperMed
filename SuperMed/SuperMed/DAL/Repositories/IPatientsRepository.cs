using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IPatientsRepository
    {
        Task<List<Patient>> GetAll();
        Task<Patient> Get(int id);
        Task<Patient> GetByName(string name);
        Task<Patient> Add(Patient patient);
        Task<Patient> Update(Patient patient);
        Task<Patient> Delete(int id);
    }
}
