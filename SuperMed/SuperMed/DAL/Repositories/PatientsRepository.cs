using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SuperMed.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Entities;

namespace SuperMed.DAL.Repositories
{
    public class PatientsRepository : IRepository<Patient>
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientsRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        //public async Task<Patient> GetPatientById(int id)
        //{
        //    return await _dbContext.Patients
        //        .FirstOrDefaultAsync(patient => patient.PatientId == id,
        //        CancellationToken.None);
        //}

        //public async Task<Patient> GetPatientByName(string patientName)
        //{
        //    return await _dbContext.Patients
        //        .FirstOrDefaultAsync(user => user.Name == patientName,
        //        CancellationToken.None);
        //}

        //public async Task<Patient> AddPatient(Patient patient)
        //{
        //    await _dbContext.Patients.AddAsync(patient, CancellationToken.None);
        //    await _dbContext.SaveChangesAsync(CancellationToken.None);
            
        //    return patient;
        //}

        //public async Task<Patient> Update(Patient patient)
        //{
        //    var entry = await GetPatientById(patient.PatientId);
        //    _dbContext.Attach(entry);

        //    entry.Phone = patient.Phone;
        //    entry.LastName = patient.LastName;

        //    await _dbContext.SaveChangesAsync();

        //    return patient;
        //}

        public async Task CreateAsync(Patient item, CancellationToken cancellationToken)
        {
            await _dbContext.Patients.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Patient> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Patients
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Patient> GetAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Patients
                .Include("Appointments")
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Patient item, CancellationToken cancellationToken)
        {
            _dbContext.Patients.Remove(item);
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Patient>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Patients.AsQueryable().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Patient> Update(Patient item, CancellationToken cancellationToken)
        {
            var entry = await GetAsync(item.Id, cancellationToken).ConfigureAwait(false);
            _dbContext.Attach(entry);

            entry.Phone = item.Phone;
            entry.LastName = item.LastName;

            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return item;
        }
    }
}
