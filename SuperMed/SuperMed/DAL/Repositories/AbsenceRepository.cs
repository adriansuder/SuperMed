using SuperMed.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMed.Entities;

namespace SuperMed.DAL.Repositories
{
    public class AbsenceRepository : IRepository<DoctorAbsence>
    {
        private readonly ApplicationDbContext _dbContext;

        public AbsenceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<DoctorAbsence> AddAbsence(DoctorAbsence doctorAbsence)
        //{
        //    await _dbContext.DoctorsAbsences.AddAsync(doctorAbsence, CancellationToken.None);
        //    await SaveChangesAsync(CancellationToken.None);

        //    return doctorAbsence;
        //}

        //public async Task<DoctorAbsence> GetDoctorsAbscenceByDate(string doctorName, DateTime date)
        //{
        //    var doctorsAbscence = await _dbContext.DoctorsAbsences
        //        .FirstOrDefaultAsync(absence =>
        //        absence.Doctor.Name == doctorName && absence.AbsenceDate == date,
        //            CancellationToken.None);

        //    return doctorsAbscence;
        //}

        //public List<DoctorAbsence> GetDoctorAbsencesToEdit(int doctorId)
        //{
        //    return _dbContext.DoctorsAbsences
        //        .Where(absence => absence.Doctor.DoctorId == doctorId && absence.AbsenceDate >= DateTime.Today)
        //        .ToList();
        //}

        //public List<DoctorAbsence> GetNextDoctorAbsences(int doctorId)
        //{
        //    return _dbContext.DoctorsAbsences
        //        .Where(absence => absence.Doctor.DoctorId == doctorId && absence.AbsenceDate >= DateTime.Today)
        //        .OrderBy(a => a.AbsenceDate)
        //        .Take(5)
        //        .ToList();
        //}

        //public async Task<DoctorAbsence> DeleteAbsence(int doctorAbsenceId)
        //{
        //    var doctorAbsence = _dbContext.DoctorsAbsences
        //        .FirstOrDefault(absence => absence.DoctorAbsenceId == doctorAbsenceId);

        //    if (doctorAbsence != null)
        //    {
        //        _dbContext.Remove(doctorAbsence);
        //        await _dbContext.SaveChangesAsync(CancellationToken.None);
        //    }

        //    return doctorAbsence;
        //}

        public async Task CreateAsync(DoctorAbsence item, CancellationToken cancellationToken)
        {
            await _dbContext.DoctorsAbsences.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<DoctorAbsence> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.DoctorsAbsences.FindAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public Task<DoctorAbsence> GetAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            return await _dbContext.DoctorsAbsences.AsQueryable().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<DoctorAbsence> Update(DoctorAbsence item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
