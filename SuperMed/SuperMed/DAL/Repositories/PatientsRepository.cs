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
            return await _dbContext.Patients
                .FirstOrDefaultAsync(patient => patient.PatientId == id,
                CancellationToken.None);
        }

        public async Task<Patient> GetPatientByName(string patientName)
        {
            return await _dbContext.Patients
                .FirstOrDefaultAsync(user => user.Name == patientName,
                CancellationToken.None);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            await _dbContext.Patients.AddAsync(patient, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            
            return patient;
        }

        public async Task<Patient> Update(Patient patient)
        {
            var entry = await GetPatientById(patient.PatientId);
            _dbContext.Attach(entry);

            entry.Phone = patient.Phone;
            entry.LastName = patient.LastName;

            await _dbContext.SaveChangesAsync();

            return patient;
        }
    }
}
