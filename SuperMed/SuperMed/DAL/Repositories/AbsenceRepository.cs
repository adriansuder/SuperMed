using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperMed.DAL.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AbsenceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DoctorAbsence> AddAbsence(DoctorAbsence doctorAbsence)
        {
            await _dbContext.DoctorsAbsences.AddAsync(doctorAbsence);
            await _dbContext.SaveChangesAsync();

            return doctorAbsence;
        }

        public async Task<DoctorAbsence> GetDoctorsAbscenceByDate(string doctorName, DateTime date)
        {
            var doctorsAbscence = await _dbContext.DoctorsAbsences.FirstOrDefaultAsync(absence =>
                absence.Doctor.Name == doctorName && absence.AbsenceDate == date);

            return doctorsAbscence;
        }

        public List<DoctorAbsence> GetDoctorAbsencesToEdit(int doctorId)
        {
            return _dbContext.DoctorsAbsences
                .Where(absence => absence.Doctor.DoctorId == doctorId && absence.AbsenceDate >= DateTime.Today)
                .ToList();
        }

        public List<DoctorAbsence> GetNextDoctorAbsences(int doctorId)
        {
            return _dbContext.DoctorsAbsences
                .Where(absence => absence.Doctor.DoctorId == doctorId && absence.AbsenceDate >= DateTime.Today)
                .OrderBy(a => a.AbsenceDate)
                .Take(5)
                .ToList();
        }

        public async Task<DoctorAbsence> DeleteAbsence(int doctorAbsenceId)
        {
            var doctorAbsence = _dbContext.DoctorsAbsences.FirstOrDefault(absence => absence.DoctorAbsenceId == doctorAbsenceId);

            if (doctorAbsence != null)
            {
                _dbContext.Remove(doctorAbsence);
                await _dbContext.SaveChangesAsync();
            }

            return doctorAbsence;
        }
    }
}
