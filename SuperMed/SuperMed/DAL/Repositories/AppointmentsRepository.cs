using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public List<Appointment> GetTodaysAppointmentByDoctorName(string docName)
        {
            return  _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .Where(a => a.Doctor.Name == docName 
                            && a.StartDateTime.ToString("d") == DateTime.Today.ToString("d"))
                .ToList();
        }

        public List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string docName)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Where(a => a.Doctor.Name == docName
                            && a.StartDateTime.Year == date.Year 
                            && a.StartDateTime.Month == date.Month 
                            && a.StartDateTime.Day == date.Day)
                .ToList();
        }

        public List<Appointment> GetDoctorsRealizedAppoinmentById(DateTime date, int docId)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .Where(a => a.Doctor.DoctorId == docId
                            && a.StartDateTime.Year <= date.Year
                            && a.StartDateTime.Month <= date.Month
                            && a.StartDateTime.Day < date.Day).OrderByDescending(x => x.StartDateTime).ToList();
        }

        public List<Appointment> GetByPatientName(string patientName)
        {
            return _dbContext.Appointments
                .Include("Patient")
                .Include("Doctor")
                .Where(a => a.Patient.Name == patientName)
                .ToList();
        }

        public List<Appointment> GetPastPatientsAppointments(string patientName)
        {
            return GetByPatientName(patientName)
                .Where(d => d.StartDateTime < DateTime.Now)
                .OrderByDescending(d => d.StartDateTime)
                .ToList();
        }

        public List<Appointment> GetUpcommingPatientsAppointments(string patientName)
        {
            return GetByPatientName(patientName)
                .Where(d => d.StartDateTime > DateTime.Now)
                .OrderBy(d => d.StartDateTime)
                .ToList();
        }

        public Task<Appointment> GetAppointmentById(int id)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task DeleteAppointmentById(int id)
        {
            var appointmentToDelete = await GetAppointmentById(id);
            _dbContext.Appointments.Remove(appointmentToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task FinishAppointment(Appointment appointment)
        {
            var entry = await GetAppointmentById(appointment.Id);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
