using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface ISpecializationsRepository
    {
        Task<List<Specialization>> GetAll();
        Task<Specialization> Get(int id);
        Task<Specialization> GetByName(string name);
        Task<Specialization> Add(Specialization specialization);
        Task<Specialization> Update(Specialization specialization);
        Task<Specialization> Delete(int id);
        Task<int> Commit();
    }
}
