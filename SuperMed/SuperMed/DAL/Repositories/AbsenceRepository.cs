using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class AbsenceRepository : IRepository<DoctorAbsence>
    {
        private readonly ApplicationDbContext _dbContext;

        public AbsenceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(DoctorAbsence item, CancellationToken cancellationToken)
        {
            await _dbContext.DoctorsAbsences.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<DoctorAbsence> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.DoctorsAbsences
                    .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task<DoctorAbsence> GetAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.DoctorsAbsences
                .FirstOrDefaultAsync(a => a.Doctor.Name == name, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(DoctorAbsence item, CancellationToken cancellationToken)
        {
            _dbContext.DoctorsAbsences.Remove(item);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
             await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<DoctorAbsence>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DoctorsAbsences
                .Include(a => a.Doctor)
                .AsQueryable()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<DoctorAbsence> Update(DoctorAbsence item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
