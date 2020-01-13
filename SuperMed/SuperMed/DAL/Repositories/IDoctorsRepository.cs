using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor> Get(int id);
        Task<Doctor> GetByName(string name);
        Task<Doctor> Add(Doctor doctor);
        Task<Doctor> Update(Doctor doctor);
        Task<Doctor> Delete(int id);
    }
}
