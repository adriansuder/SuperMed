using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor> GetDoctorByName(string name);
        Task<Doctor> Add(Doctor doctor);
    }
}
