using System;
using System.Threading;
using System.Threading.Tasks;
using SuperMed.Entities;
using SuperMed.Models.ViewModels;

namespace SuperMed.Services
{
    public interface IAppService
    {
        Task<Appointment> GetAppointmentById(int appointmentId, CancellationToken cancellationToken);
        Task<Appointment> GetAppointmentByPatientName(string patientName, CancellationToken cancellationToken);

        Task<EditAppointmentViewModel> EditAppointment(int appointmentId, string name, CancellationToken cancellationToken);
        Task<EditAppointmentViewModel> EditAppointment(int appointmentId, EditAppointmentViewModel model, CancellationToken cancellationToken);
        Task DeleteAppointmentById(int id, string name, CancellationToken cancellationToken);
        Task<DoctorsViewModel> GetDoctorsAppointmentsForDay(string name, DateTime date, CancellationToken cancellationToken);
        Task<DoctorAppointmentHistoryViewModel> GetDoctorsRealizedAppointments(string name, CancellationToken cancellationToken);
        Task<EditDoctorAbsencesViewModel> GetDoctorsAbsencesToEdit(string name, CancellationToken cancellationToken);
        Task AddDoctorsAbsence(string name, DateTime modelAbsenceDate, string modelAbsenceDescription, CancellationToken cancellationToken);
        Task DeleteAbsenceById(int id, CancellationToken cancellationToken);
        Task<PatientViewModel> GetPatientsAppointments(string name, CancellationToken cancellationToken);
        Task<CreateVisitViewModel> CreateVisit(CancellationToken cancellationToken);
        Task<bool> GetHasDoctorAbsenceOnDate(string modelDoctorName, DateTime modelStartDateTime, CancellationToken cancellationToken);
        Task<CreateVisitStep2ViewModel> CreateVisitStepTwo(CreateVisitViewModel model, CancellationToken cancellationToken);
        Task<CreateVisitStep3ViewModel> CreateVisitStepThree(CreateVisitStep2ViewModel model, CancellationToken cancellationToken);
        Task SubmitVisit(string name, CreateVisitStep3ViewModel model, CancellationToken cancellationToken);
        Task<ChangePatientInfoViewModel> ChangePatientInfo(string name, CancellationToken cancellationToken);
        Task SaveChangedPatientInfo(string name, ChangePatientInfoViewModel changePatientInfoViewModel, CancellationToken cancellationToken);
        Task AddPatient(Patient patient, CancellationToken cancellationToken);
        Task<Specialization> GetSpecializationByName(string modelSpecialization, CancellationToken cancellationToken);
        Task AddSpecialization(Specialization specialization, CancellationToken cancellationToken);
        Task AddDoctor(Doctor doctor, CancellationToken cancellationToken);
        Task<PatientAppointmentHistoryViewModel> GetPatientsRealizedAppointments(string name, CancellationToken cancellationToken);
    }
}
