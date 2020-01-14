using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.DAL.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<Appointment> Add(Appointment appointment);
        List<Appointment> GetTodaysAppointmentByDoctorName(string docName);
        List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string docName); 
        List<Appointment> GetByPatientName(string docName);
        List<Appointment> GetPastPatientsAppointments(string patientName);
        List<Appointment> GetUpcommingPatientsAppointments(string patientName);
        List<Appointment> GetDoctorsRealizedAppoinmentById(DateTime date, int docId);
        List<Appointment> GetDoctorsAppointmentsByDate(DateTime date, string docName);
        List<Appointment> GetPastPatientsAppointments(string patientName);
        List<Appointment> GetUpcommingPatientsAppointments(string patientName);
        Task<Appointment> GetAppointmentById(int id);
    }
}
