using System;
using System.Collections.Generic;
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

        public async Task<DoctorAbsence> Add(DoctorAbsence doctorAbsence)
        {
            await _dbContext.DoctorsAbsences.AddAsync(doctorAbsence);
            await _dbContext.SaveChangesAsync();

            return doctorAbsence;
        }

    }
}
