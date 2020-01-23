using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class AppointmentsRepository : IRepository<Appointment>
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Appointment item, CancellationToken cancellationToken)
        {
            await _dbContext.Appointments.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<Appointment> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments
                .Include("Patient")
                .Include("Doctor")
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<Appointment> GetAsync(string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(Appointment item, CancellationToken cancellationToken)
        {
            _dbContext.Appointments.Remove(item);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Appointment>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments
                .Include("Patient")
                .Include("Doctor")
                .AsQueryable()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Appointment> Update(Appointment item, CancellationToken cancellationToken)
        {
            var entry = await GetAsync(item.Id, cancellationToken).ConfigureAwait(false);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
