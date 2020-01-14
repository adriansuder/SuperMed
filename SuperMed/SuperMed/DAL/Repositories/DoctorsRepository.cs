using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;

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
            var doctors = await _dbContext.Doctors.ToListAsync();
            
            return doctors;
        }

        public async Task<Doctor> GetDoctorByName(string name)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Doctor> Add(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();
            
            return doctor;
        }
    }
}
