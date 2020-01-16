using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public interface IAbsenceRepository
    {
        Task<DoctorAbsence> AddAsync(DoctorAbsence doctorAbsence);
        DoctorAbsence GetDoctorsAbscenceByDate(string doctorName, DateTime date);
        Task<DoctorAbsence> DeleteAbsence(int doctorAbsenceId);
        List<DoctorAbsence> GetDoctorAbsencesToEdit(int docId);
        List<DoctorAbsence> GetNextDoctorAbsences(int docId);

    }
}
