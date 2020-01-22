using SuperMed.Entities;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface ISpecializationsRepository
    {
        Task<Specialization> AddSpecialization(Specialization specialization);
        Task<Specialization> GetSpecializationById(int specializationId);
        Task<Specialization> GetSpecializationByUserName(string specializationName);
    }
}
