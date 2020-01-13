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
            var docs = await _dbContext.Doctors.ToListAsync();
            return docs;
        }

        public async Task<Doctor> Get(int id)
        {
            //throw new System.NotImplementedException();
            return await _dbContext.Doctors.FindAsync(id);
        }

        public async Task<Doctor> GetByName(string name)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Doctor> Add(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();
            return doctor;
        }

        public Task<Doctor> Update(Doctor patient)
        {
            throw new System.NotImplementedException();
        }

        public Task<Doctor> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
