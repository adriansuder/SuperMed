using System;
using System.Linq;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

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
    }
}
