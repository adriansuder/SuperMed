using Microsoft.EntityFrameworkCore;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            var doctors = await _dbContext.Doctors.ToListAsync(CancellationToken.None);
            
            return doctors;
        }

        public async Task<Doctor> GetDoctorByName(string doctorName)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(user => user.Name == doctorName);
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            await _dbContext.Doctors.AddAsync(doctor, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            
            return doctor;
        }
    }
}
