using Microsoft.EntityFrameworkCore;
using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.DAL.Repositories.Interfaces;

namespace SuperMed.DAL.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            return appointment;
        }

        public List<Appointment> GetTodaysAppointmentByDoctorName(string doctorName)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .Where(appointment => appointment.Doctor.Name == doctorName &&
                                      appointment.StartDateTime.ToString("d") == DateTime.Today.ToString("d"))
                .ToList();
        }

        public List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string doctorName)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Where(appointment => appointment.Doctor.Name == doctorName &&
                                      appointment.StartDateTime.Year == date.Year &&
                                      appointment.StartDateTime.Month == date.Month &&
                                      appointment.StartDateTime.Day == date.Day)
                .ToList();
        }

        public List<Appointment> GetDoctorsRealizedAppoinmentById(DateTime date, int doctorId)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .Where(appointment => appointment.Doctor.DoctorId == doctorId &&
                                      appointment.StartDateTime.Year <= date.Year &&
                                      appointment.StartDateTime.Month <= date.Month &&
                                      appointment.StartDateTime.Day < date.Day)
                .OrderByDescending(x => x.StartDateTime)
                .ToList();
        }

        public List<Appointment> GetPastPatientsAppointments(string patientName)
        {
            return GetAppointmentByPatientName(patientName)
                .Where(appointment => appointment.StartDateTime < DateTime.Now)
                .OrderByDescending(appointment => appointment.StartDateTime)
                .ToList();
        }

        public List<Appointment> GetUpcommingPatientsAppointments(string patientName)
        {
            return GetAppointmentByPatientName(patientName)
                .Where(appointment => appointment.StartDateTime > DateTime.Now)
                .OrderBy(appointment => appointment.StartDateTime)
                .ToList();
        }

        public Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return _dbContext.Appointments
                .Include("Doctor")
                .Include("Patient")
                .FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);
        }

        public async Task DeleteAppointmentById(int appointmentId)
        {
            var appointmentToDelete = await GetAppointmentById(appointmentId);
            _dbContext.Appointments.Remove(appointmentToDelete);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task FinishAppointment(Appointment appointment)
        {
            var entry = await GetAppointmentById(appointment.Id);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        private IEnumerable<Appointment> GetAppointmentByPatientName(string patientName)
        {
            return _dbContext.Appointments
                .Include("Patient")
                .Include("Doctor")
                .Where(appointment => appointment.Patient.Name == patientName)
                .ToList();
        }
    }
}
