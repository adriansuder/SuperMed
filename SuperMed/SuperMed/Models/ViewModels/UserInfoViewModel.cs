using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
