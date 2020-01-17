using Microsoft.EntityFrameworkCore;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class SpecializationsRepository : ISpecializationsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecializationsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<Specialization> GetSpecializationById(int specializationId)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(i => i.Id == specializationId);
        }

        public async Task<Specialization> GetSpecializationByUserName(string specializationName)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(user => user.Name == specializationName);
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
