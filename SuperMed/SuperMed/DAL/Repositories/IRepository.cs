using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IRepository
    {
        Task<List<Patient>> GetAll();
        Task<Patient> Get(int id);
        Task<Patient> Add(Patient patient);
        Task<Patient> Update(Patient patient);
        Task<Patient> Delete(int id);
    }
}
