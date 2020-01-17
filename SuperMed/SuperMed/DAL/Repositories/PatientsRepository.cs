using Microsoft.EntityFrameworkCore;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task<Patient> GetPatientByName(string patientName)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(user => user.Name == patientName);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            
            return patient;
        }

        public async Task<Patient> Update(Patient patient)
        {
            var entry = await GetPatientById(patient.PatientId);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return patient;
        }
    }
}
