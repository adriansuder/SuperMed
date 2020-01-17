using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IDoctorsRepository
    {
        Task<Doctor> AddDoctor(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor> GetDoctorByName(string doctorName);
    }
}
