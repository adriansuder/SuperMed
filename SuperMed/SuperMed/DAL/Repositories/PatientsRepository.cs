using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class PatientsRepository : IRepository<Patient>
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task CreateAsync(Patient item, CancellationToken cancellationToken)
        {
            await _dbContext.Patients.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Patient> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Patients
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Patient> GetAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Patients
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Patient item, CancellationToken cancellationToken)
        {
            _dbContext.Patients.Remove(item);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Patient>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Patients
                .Include("Patient")
                .AsQueryable()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Patient> Update(Patient item, CancellationToken cancellationToken)
        {
            var entry = await GetAsync(item.Id, cancellationToken).ConfigureAwait(false);
            _dbContext.Attach(entry);

            entry.Phone = item.Phone;
            entry.LastName = item.LastName;

            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return item;
        }
    }
}
