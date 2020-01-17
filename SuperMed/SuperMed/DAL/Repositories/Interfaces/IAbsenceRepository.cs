using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IAbsenceRepository
    {
        Task<DoctorAbsence> AddAbsence(DoctorAbsence doctorAbsence);
        Task<DoctorAbsence> DeleteAbsence(int doctorAbsenceId);
        Task<DoctorAbsence> GetDoctorsAbscenceByDate(string doctorName, DateTime date);
        List<DoctorAbsence> GetDoctorAbsencesToEdit(int doctorId);
        List<DoctorAbsence> GetNextDoctorAbsences(int doctorId);
    }
}
