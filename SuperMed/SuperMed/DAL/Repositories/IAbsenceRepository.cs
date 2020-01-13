using SuperMed.Models.Entities;
using System;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public interface IAbsenceRepository
    {
        Task<DoctorAbsence> AddAsync(DoctorAbsence doctorAbsence);
        DoctorAbsence GetDoctorsAbscenceByDate(string doctorName, DateTime date);
    }
}
