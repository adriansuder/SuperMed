using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class DoctorsRepository : IRepository<Doctor>
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task CreateAsync(Doctor item, CancellationToken cancellationToken)
        {
            await _dbContext.Doctors.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Doctor> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Doctors
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Doctor> GetAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Doctors
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Doctor item, CancellationToken cancellationToken)
        {
            _dbContext.Doctors.Remove(item);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Doctor>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Doctors
                .Include("Specialization")
                .AsQueryable()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<Doctor> Update(Doctor item, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
