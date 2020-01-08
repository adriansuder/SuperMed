using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories
{
    public interface IAbsenceRepository
    {
        Task<DoctorAbsence> Add(DoctorAbsence doctorAbsence);
    }
}
