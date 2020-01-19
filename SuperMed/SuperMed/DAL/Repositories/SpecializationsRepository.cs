using Microsoft.EntityFrameworkCore;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using System.Threading;
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
            return await _dbContext.Specializations
                .FirstOrDefaultAsync(i => i.Id == specializationId,
                CancellationToken.None);
        }

        public async Task<Specialization> GetSpecializationByUserName(string specializationName)
        {
            return await _dbContext.Specializations
                .FirstOrDefaultAsync(user => user.Name == specializationName,
                CancellationToken.None);
        }

        public async Task<Specialization> AddSpecialization(Specialization specialization)
        {
            var spec = _dbContext.Specializations
                .FirstOrDefaultAsync(s => s.Name == specialization.Name,
                CancellationToken.None);

            if (spec == null)
            {
                await _dbContext.Specializations.AddAsync(specialization, CancellationToken.None);
                await _dbContext.SaveChangesAsync(CancellationToken.None);
            }

            return specialization;
        }
    }
}
