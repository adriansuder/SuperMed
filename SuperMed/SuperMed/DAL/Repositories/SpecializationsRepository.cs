using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public class SpecializationsRepository : ISpecializationsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecializationsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<Specialization> GetSpecializationById(int id)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Specialization> GetSpecializationByUserName(string name)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Specialization> AddSpecialization(Specialization specialization)
        {
            var spec = _dbContext.Specializations.FirstOrDefault(s => s.Name == specialization.Name);

            if (spec == null)
            {
                _dbContext.Specializations.Add(specialization);
                await _dbContext.SaveChangesAsync();
            }

            return spec;
        }
    }
}
