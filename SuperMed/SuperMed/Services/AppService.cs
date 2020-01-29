using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMed.DAL.Repositories;
using SuperMed.Entities;
using SuperMed.Managers;
using SuperMed.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.Services
{
    public class AppService : IAppService
    {
        private readonly IRepository<DoctorAbsence> _absenceRepository;
        private readonly IRepository<Appointment> _appointmentsRepository;
        private readonly IRepository<Doctor> _doctorsRepository;
        private readonly IRepository<Patient> _patientsRepository;
        private readonly IRepository<Specialization> _specializationsRepository;

        public AppService(
            IRepository<DoctorAbsence> absenceRepository,
            IRepository<Appointment> appointmentsRepository,
            IRepository<Doctor> doctorsRepository,
            IRepository<Patient> patientsRepository,
            IRepository<Specialization> specializationsRepository)
        {
            _absenceRepository = absenceRepository;
            _appointmentsRepository = appointmentsRepository;
            _doctorsRepository = doctorsRepository;
            _patientsRepository = patientsRepository;
            _specializationsRepository = specializationsRepository;
        }

        public async Task<Appointment> GetAppointmentById(int appointmentId, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.GetAsync(appointmentId, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Appointment> GetAppointmentByPatientName(string patientName, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentsRepository.ListAsync(cancellationToken);
            return appointments.FirstOrDefault(appointment => appointment.Patient.Name == patientName);
        }

        public async Task<EditAppointmentViewModel> EditAppointment(int appointmentId, string name, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetAsync(appointmentId, cancellationToken).ConfigureAwait(false);
            var isDoctor = await _doctorsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false) != null;

            if (appointment.Patient.Name != name && appointment.Doctor.Name != name)
            {
                return null;
            }

            return new EditAppointmentViewModel
            {
                Appointment = appointment,
                IsDoctor = isDoctor
            };
        }

        public async Task<EditAppointmentViewModel> EditAppointment(int appointmentId, EditAppointmentViewModel model, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetAsync(appointmentId, cancellationToken).ConfigureAwait(false);

            appointment.Review = model.Appointment.Review;
            appointment.AppointmentStatus = AppointmentStatus.Finished;

            await _appointmentsRepository.Update(appointment, cancellationToken);

            return new EditAppointmentViewModel
            {
                Appointment = appointment
            };
        }

        public async Task DeleteAppointmentById(int id, string name, CancellationToken cancellationToken)
        {
            var appointmentToDelete = await _appointmentsRepository.GetAsync(id, cancellationToken);

            if (appointmentToDelete.Patient.Name == name)
            {
                await _appointmentsRepository.DeleteAsync(appointmentToDelete, cancellationToken);
            }
        }

        public async Task<DoctorsViewModel> GetDoctorsAppointmentsForDay(string name, DateTime date, CancellationToken cancellationToken)
        {
            var doctor = await _doctorsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false);
            var appointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var absences = await _absenceRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            var today = appointments.Where(a => a.Doctor.Id == doctor.Id && a.StartDateTime.ToString("d") == date.ToString("d")).ToList();
            var tomorrow = appointments.Where(a =>  a.Doctor.Id == doctor.Id && a.StartDateTime.ToString("d") == date.AddDays(1).ToString("d")).ToList();
            today.AddRange(tomorrow);

            var doctorsAbsences = absences.Where(a => a.Doctor.Id == doctor.Id && a.AbsenceDate > date).OrderBy(a => a.AbsenceDate).Take(5).ToList();

            return new DoctorsViewModel
            {
                Appointments = today,
                NextDoctorsAbsences = doctorsAbsences
            };
        }

        public async Task<DoctorAppointmentHistoryViewModel> GetDoctorsRealizedAppointments(string name, CancellationToken cancellationToken)
        {
            var doctorsAppointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctorsRealizedAppointments = doctorsAppointments
                .Where(a => a.Doctor.Name == name && a.StartDateTime.Year <= DateTime.Now.Year &&
                            a.StartDateTime.Month <= DateTime.Now.Month && a.StartDateTime.Day < DateTime.Now.Day)
                .OrderByDescending(a => a.StartDateTime).ToList();

            return new DoctorAppointmentHistoryViewModel
            {
                RealizedAppointments = doctorsRealizedAppointments
            };
        }

        public async Task<EditDoctorAbsencesViewModel> GetDoctorsAbsencesToEdit(string name, CancellationToken cancellationToken)
        {
            var doctorsAbsences = await _absenceRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctorsUpcommingAbscences = doctorsAbsences.Where(a => a.Doctor.Name == name && a.AbsenceDate >= DateTime.Today).ToList();

            return new EditDoctorAbsencesViewModel
            {
                DoctorAbsences = doctorsUpcommingAbscences
            };
        }

        public async Task AddDoctorsAbsence(string name, DateTime modelAbsenceDate, string modelAbsenceDescription,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctorsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false);
            var doctorsAbsences = await _absenceRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctorsAppointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            if (doctorsAbsences.Any(a => a.AbsenceDate == modelAbsenceDate) 
                || doctorsAppointments.Any(a => a.Doctor.Name == name && a.StartDateTime.ToString("d") == modelAbsenceDate.ToString("d")))
            {
                return;
            }

            var absenceToAdd = new DoctorAbsence
            {
                Doctor = doctor,
                AbsenceDate = modelAbsenceDate,
                AbsenceDescription = modelAbsenceDescription
            };

            await _absenceRepository.CreateAsync(absenceToAdd, cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAbsenceById(int id, CancellationToken cancellationToken)
        {
            var absenceToDelete = await _absenceRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            await _absenceRepository.DeleteAsync(absenceToDelete, cancellationToken).ConfigureAwait(false);
        }

        public async Task<PatientViewModel> GetPatientsAppointments(string name, CancellationToken cancellationToken)
        {
            var patient = await _patientsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false);
            var patientsAppointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            return new PatientViewModel
            {
                Patient = patient,
                GetPastAppointments = patientsAppointments.Where(a => a.Patient.Name == name && a.StartDateTime < DateTime.Today).OrderByDescending(a => a.StartDateTime).ToList(),
                GetUpcommingAppointments = patientsAppointments.Where(a => a.Patient.Name == name && a.StartDateTime >= DateTime.Today).OrderBy(a => a.StartDateTime).ToList()
            };
        }

        public async Task<CreateVisitViewModel> CreateVisit(CancellationToken cancellationToken)
        {
            var doctors = await _doctorsRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            var listItems = new List<SelectListItem>();

            foreach (var doctor in doctors)
            {
                listItems.Add(new SelectListItem
                {
                    Value = doctor.Name,
                    Text = $"{doctor.Specialization.Name} - {doctor.FirstName} {doctor.LastName}"
                });
            }

            return new CreateVisitViewModel
            {
                StartDateTime = DateTime.Now,
                Doctors = listItems
            };
        }

        public async Task<bool> GetHasDoctorAbsenceOnDate(string modelDoctorName, DateTime modelStartDateTime,
            CancellationToken cancellationToken)
        {
            var doctorAbsences = await _absenceRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            return doctorAbsences.Any(a => a.Doctor.Name == modelDoctorName && a.AbsenceDate == modelStartDateTime);
        }

        public async Task<CreateVisitStep2ViewModel> CreateVisitStepTwo(CreateVisitViewModel model, CancellationToken cancellationToken)
        {
            var doctor = await _doctorsRepository.GetAsync(model.DoctorName, cancellationToken).ConfigureAwait(false);

            var doctorsAppointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctorsAppointmentsByDate = doctorsAppointments.Where(a =>
                a.Doctor.Name == model.DoctorName && a.StartDateTime.Year == model.StartDateTime.Year &&
                a.StartDateTime.Month == model.StartDateTime.Month && a.StartDateTime.Day == model.StartDateTime.Day);
            
            var dates = AppointmentManager.GetAvailableTimes(model.StartDateTime);

            foreach (var date in doctorsAppointmentsByDate)
            {
                dates.RemoveAll(dateTime => dateTime.TimeOfDay == date.StartDateTime.TimeOfDay);
            }

            return new CreateVisitStep2ViewModel
            {
                Date = dates,
                Doctor = doctor,
                DoctorName = model.DoctorName,
                StartDateTime = model.StartDateTime,
                Description = model.Description
            };
        }

        public async Task<CreateVisitStep3ViewModel> CreateVisitStepThree(CreateVisitStep2ViewModel model, CancellationToken cancellationToken)
        {
            var doctor = await _doctorsRepository.GetAsync(model.DoctorName, cancellationToken).ConfigureAwait(false);

            return new CreateVisitStep3ViewModel
            {
                StartDateTime = new DateTime(model.StartDateTime.Year, model.StartDateTime.Month,
                    model.StartDateTime.Day, model.TimeOfDay.Hour, model.TimeOfDay.Minute, 0),
                Doctor = doctor,
                DoctorName = model.DoctorName,
                Description = model.Description,
                TimeOfDay = model.TimeOfDay
            };
        }

        public async Task SubmitVisit(string name, CreateVisitStep3ViewModel model, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctor = await _doctorsRepository.GetAsync(model.DoctorName, cancellationToken).ConfigureAwait(false);
            var startDateTime = new DateTime(model.StartDateTime.Year, model.StartDateTime.Month,
                model.StartDateTime.Day, model.TimeOfDay.Hour, model.TimeOfDay.Minute, 0);

            var hasAlready = appointments.Any(a => a.Doctor.Name == model.DoctorName && a.StartDateTime == startDateTime);

            if (hasAlready)
            {
                throw new Exception("Doktor jest w tym czasie zajęty.");
            }

            var patient = await _patientsRepository.GetAsync(name, cancellationToken);

            var appointment = new Appointment
            {
                StartDateTime = startDateTime,
                Doctor = doctor,
                Patient = patient,
                AppointmentStatus = AppointmentStatus.New,
                Description = model.Description
            };

            await _appointmentsRepository.CreateAsync(appointment, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ChangePatientInfoViewModel> ChangePatientInfo(string name, CancellationToken cancellationToken)
        {
            var patient = await _patientsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false);

            return new ChangePatientInfoViewModel
            {
                Name = name,
                LastName = patient.LastName,
                Phone = patient.Phone
            };
        }

        public async Task SaveChangedPatientInfo(string name, ChangePatientInfoViewModel changePatientInfoViewModel,
            CancellationToken cancellationToken)
        {
            var patient = await _patientsRepository.GetAsync(name, cancellationToken).ConfigureAwait(false);

            patient.LastName = changePatientInfoViewModel.LastName;
            patient.Phone = changePatientInfoViewModel.Phone;

            await _patientsRepository.Update(patient, cancellationToken).ConfigureAwait(false); 
        }

        public async Task AddPatient(Patient patient, CancellationToken cancellationToken)
        {
            await _patientsRepository.CreateAsync(patient, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Specialization> GetSpecializationByName(string modelSpecialization, CancellationToken cancellationToken)
        {
            var listOfSpecializations =
                await _specializationsRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            return listOfSpecializations.FirstOrDefault(s => s.Name == modelSpecialization);
        }

        public async Task AddSpecialization(Specialization specialization, CancellationToken cancellationToken)
        {
            await _specializationsRepository.CreateAsync(specialization, cancellationToken).ConfigureAwait(false);
        }

        public async Task AddDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            await _doctorsRepository.CreateAsync(doctor, cancellationToken).ConfigureAwait(false);
        }

        public async Task<PatientAppointmentHistoryViewModel> GetPatientsRealizedAppointments(string name, CancellationToken cancellationToken)
        {
            var doctorsAppointments = await _appointmentsRepository.ListAsync(cancellationToken).ConfigureAwait(false);
            var doctorsRealizedAppointments = doctorsAppointments
                .Where(a => a.Patient.Name == name && a.StartDateTime.Year <= DateTime.Now.Year &&
                            a.StartDateTime.Month <= DateTime.Now.Month && a.StartDateTime.Day < DateTime.Now.Day)
                .OrderByDescending(a => a.StartDateTime).ToList();

            return new PatientAppointmentHistoryViewModel
            {
                RealizedAppointments = doctorsRealizedAppointments
            };
        }
    }
}
