using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class SpecializationsRepository : IRepository<Specialization>
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecializationsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task CreateAsync(Specialization item, CancellationToken cancellationToken)
        {
            await _dbContext.Specializations.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Specialization> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Specializations
                .FirstOrDefaultAsync(p => p.SpecializationId == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<Specialization> GetAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Specialization item, CancellationToken cancellationToken)
        {
            _dbContext.Specializations.Remove(item);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Specialization>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Specializations
                .AsQueryable()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<Specialization> Update(Specialization item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
