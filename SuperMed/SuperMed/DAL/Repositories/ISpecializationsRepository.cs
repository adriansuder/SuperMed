using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface ISpecializationsRepository
    {
        Task<Specialization> GetSpecializationById(int id);
        Task<Specialization> GetSpecializationByUserName(string name);
        Task<Specialization> AddSpecialization(Specialization specialization);
    }
}
