using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.DAL.Repositories.Interfaces;
using SuperMed.Entities;

namespace SuperMed.DAL.Repositories
{
    public class AppointmentsRepository : IRepository<Appointment>
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<Appointment> AddAppointment(Appointment appointment)
        //{
        //    await _dbContext.Appointments.AddAsync(appointment, CancellationToken.None);
        //    await _dbContext.SaveChangesAsync(CancellationToken.None);

        //    return appointment;
        //}

        //public List<Appointment> GetTodaysAppointmentByDoctorName(string doctorName)
        //{
        //    return _dbContext.Appointments
        //        .Include("Doctor")
        //        .Include("Patient")
        //        .Where(appointment => appointment.Doctor.Name == doctorName &&
        //                              appointment.StartDateTime.ToString("d") == DateTime.Today.ToString("d"))
        //        .ToList();
        //}

        //public List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string doctorName)
        //{
        //    return _dbContext.Appointments
        //        .Include("Doctor")
        //        .Where(appointment => appointment.Doctor.Name == doctorName &&
        //                              appointment.StartDateTime.Year == date.Year &&
        //                              appointment.StartDateTime.Month == date.Month &&
        //                              appointment.StartDateTime.Day == date.Day)
        //        .ToList();
        //}

        //public List<Appointment> GetDoctorsRealizedAppoinmentById(DateTime date, int doctorId)
        //{
        //    return _dbContext.Appointments
        //        .Include("Doctor")
        //        .Include("Patient")
        //        .Where(appointment => appointment.Doctor.DoctorId == doctorId &&
        //                              appointment.StartDateTime.Year <= date.Year &&
        //                              appointment.StartDateTime.Month <= date.Month &&
        //                              appointment.StartDateTime.Day < date.Day)
        //        .OrderByDescending(x => x.StartDateTime)
        //        .ToList();
        //}

        //public List<Appointment> GetPastPatientsAppointments(string patientName)
        //{
        //    return GetAppointmentByPatientName(patientName)
        //        .Where(appointment => appointment.StartDateTime < DateTime.Now)
        //        .OrderByDescending(appointment => appointment.StartDateTime)
        //        .ToList();
        //}

        //public List<Appointment> GetUpcommingPatientsAppointments(string patientName)
        //{
        //    return GetAppointmentByPatientName(patientName)
        //        .Where(appointment => appointment.StartDateTime > DateTime.Now)
        //        .OrderBy(appointment => appointment.StartDateTime)
        //        .ToList();
        //}

        //public Task<Appointment> GetAppointmentById(int appointmentId)
        //{
        //    return _dbContext.Appointments
        //        .Include("Doctor")
        //        .Include("Patient")
        //        .FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);
        //}

        //public async Task DeleteAppointmentById(int appointmentId)
        //{
        //    var appointmentToDelete = await GetAppointmentById(appointmentId);
        //    _dbContext.Appointments.Remove(appointmentToDelete);
        //    await _dbContext.SaveChangesAsync(CancellationToken.None);
        //}

        //public async Task FinishAppointment(Appointment appointment)
        //{
        //    var entry = await GetAppointmentById(appointment.Id);
        //    _dbContext.Entry(entry).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync(CancellationToken.None);
        //}

        //private IEnumerable<Appointment> GetAppointmentByPatientName(string patientName)
        //{
        //    return _dbContext.Appointments
        //        .Include("Patient")
        //        .Include("Doctor")
        //        .Where(appointment => appointment.Patient.Name == patientName)
        //        .ToList();
        //}

        public async Task CreateAsync(Appointment item, CancellationToken cancellationToken)
        {
            await _dbContext.Appointments.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<Appointment> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments
                .Include("Patient")
                .Include("Doctor")
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<Appointment> GetAsync(string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(Appointment item, CancellationToken cancellationToken)
        {
            _dbContext.Appointments.Remove(item);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Appointment>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments.Include("Patient").Include("Doctor").AsQueryable().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Appointment> Update(Appointment item, CancellationToken cancellationToken)
        {
            var entry = await GetAsync(item.Id, cancellationToken).ConfigureAwait(false);
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
