using System.Threading;
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

        public async Task<Patient> GetAppointmentByPatientId(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task<Patient> GetPatientByName(string name)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<Patient> Add(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            
            return patient;
        }

        public async Task<Patient> Update(Patient patient)
        {
            var entry = await GetAppointmentByPatientId(patient.PatientId);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return patient;
        }
    }
}
