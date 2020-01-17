using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<Appointment> AddAppointment(Appointment appointment);
        List<Appointment> GetTodaysAppointmentByDoctorName(string doctorName);
        List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string doctorName);
        List<Appointment> GetPastPatientsAppointments(string patientName);
        List<Appointment> GetUpcommingPatientsAppointments(string patientName);
        List<Appointment> GetDoctorsRealizedAppoinmentById(DateTime date, int doctorId);
        Task<Appointment> GetAppointmentById(int appointmentId);
        Task DeleteAppointmentById(int appointmentId);
        Task FinishAppointment(Appointment appointment);
    }
}
