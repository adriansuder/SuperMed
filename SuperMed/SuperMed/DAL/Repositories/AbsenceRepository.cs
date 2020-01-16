using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AbsenceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DoctorAbsence> AddAsync(DoctorAbsence doctorAbsence)
        {
            await _dbContext.DoctorsAbsences.AddAsync(doctorAbsence);
            await _dbContext.SaveChangesAsync();

            return doctorAbsence;
        }

        public DoctorAbsence GetDoctorsAbscenceByDate(string doctorName, DateTime date)
        {
            var doctorsAbscence =
                _dbContext.DoctorsAbsences.FirstOrDefault(a => a.Doctor.Name == doctorName && a.AbsenceDate == date);

            return doctorsAbscence;
        }

        public List<DoctorAbsence> GetDoctorAbsencesToEdit(int docId)
        {
            return _dbContext.DoctorsAbsences
                .Where(a => a.Doctor.DoctorId == docId && a.AbsenceDate >= DateTime.Today).ToList();
        }

        public List<DoctorAbsence> GetNextDoctorAbsences(int docId)
        {
            return _dbContext.DoctorsAbsences
                .Where(a => a.Doctor.DoctorId == docId && a.AbsenceDate >= DateTime.Today).OrderBy(a => a.AbsenceDate).Take(5).ToList();
        }

        public string GetAbsenceId(DoctorAbsence doctorAbsence)
        {
            return _dbContext.DoctorsAbsences.Where(a => a.DoctorAbsenceId == doctorAbsence.DoctorAbsenceId).ToString();
        }

        public async Task<DoctorAbsence> DeleteAbsence(int doctorAbsenceId)
        {
            DoctorAbsence doctorAbsence = _dbContext.DoctorsAbsences.Where(x => x.DoctorAbsenceId == doctorAbsenceId).First();
             _dbContext.Remove(doctorAbsence);
            _dbContext.SaveChanges();

            return doctorAbsence;
        }
    }
}
