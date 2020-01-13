using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.Managers
{
    public class AppointmentManager
    {
        public AppointmentManager()
        {
            var time = DateTime.Now;

            // jezeli liczba appointments na ten dzien = 0 => ustaw: dzisiaj, 8:00
            if (time.Hour < 8)
            {

            }
        }

        public List<DateTime> GenerateDateTimes(DateTime from)
        {
            var ret = new List<DateTime>();

            var now = DateTime.Now;
            DateTime startTime;

            if (from.Year == now.Year && from.Month == now.Month && from.Day == now.Day)
            {
                startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 45, 0);
            }
            else
            {
                startTime = new DateTime(from.Year, from.Month, from.Day, 7, 45, 0);
            }

            double fifteen = 15;

            for (int i = 0; i < 32; i++)
            {
                var toAdd = startTime.AddMinutes(fifteen);
                
                ret.Add(toAdd);
                fifteen += 15;
            }

            ret.RemoveAll(c => c.Hour == 12);

            return ret;
        }

        public List<DateTime> GetNearest(DateTime from)
        {
            var now = DateTime.Now;

            var nearest = GenerateDateTimes(from);
            if (from.Year == now.Year && from.Month == now.Month && from.Day == now.Day)
            {
                nearest.RemoveAll(c => c.TimeOfDay < now.TimeOfDay);
            }

            return nearest;
        }
    }
}
