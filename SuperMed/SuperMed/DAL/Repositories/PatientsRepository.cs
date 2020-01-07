using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public Task<List<Patient>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Patient> Get(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task<Patient> GetByName(string name)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Patient> Add(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }

        public Task<Patient> Update(Patient patient)
        {
            throw new System.NotImplementedException();
        }

        public Task<Patient> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
