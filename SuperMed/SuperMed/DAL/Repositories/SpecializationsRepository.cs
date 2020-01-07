using System.Collections.Generic;
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

        public Task<List<Specialization>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Specialization> Get(int id)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Specialization> GetByName(string name)
        {
            return await _dbContext.Specializations.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Specialization> Add(Specialization specialization)
        {
            var spec = _dbContext.Specializations.FirstOrDefault(s => s.Name == specialization.Name);

            if (spec == null)
            {
                _dbContext.Specializations.Add(specialization);
                await _dbContext.SaveChangesAsync();
            }

            return spec;
        }

        public Task<Specialization> Update(Specialization specialization)
        {
            throw new System.NotImplementedException();
        }

        public Task<Specialization> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
