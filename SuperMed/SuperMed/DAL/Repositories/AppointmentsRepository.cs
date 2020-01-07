using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Appointment> Add(Appointment appointment)
        {
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            return appointment;
        }

        public async Task<List<Appointment>> GetByDoctorName(string docName)
        {
            return  _dbContext.Appointments.Where(a => a.Doctor.Name == docName && a.StartDateTime.ToString("M/d/yyyy") == DateTime.Now.ToString("M/d/yyyy")).ToList();
        }

        public async Task<List<Appointment>> GetByPatientName(string patientName)
        {
            return _dbContext.Appointments.Where(a => a.Patient.Name == patientName).ToList();
        }
    }
}
