using System;
using System.Collections.Generic;

namespace SuperMed.Managers
{
    public static class AppointmentManager
    {
        public static List<DateTime> GetAvailableTimes(DateTime from)
        {
            var now = DateTime.Now;

            var availableTimes = GenerateDateTimes(from);

            if (from.Year == now.Year && from.Month == now.Month && from.Day == now.Day)
            {
                availableTimes.RemoveAll(dateTime => dateTime.TimeOfDay < now.TimeOfDay);
            }

            return availableTimes;
        }

        private static List<DateTime> GenerateDateTimes(DateTime from)
        {
            var ret = new List<DateTime>();

            var now = DateTime.Now;
            DateTime startTime;
            double fifteen = 15;

            if (from.Year == now.Year && from.Month == now.Month && from.Day == now.Day)
            {
                startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 45, 0);
            }
            else
            {
                startTime = new DateTime(from.Year, from.Month, from.Day, 7, 45, 0);
            }

            for (var i = 0; i < 32; i++)
            {
                var toAdd = startTime.AddMinutes(fifteen);
                ret.Add(toAdd);
                fifteen += 15;
            }

            ret.RemoveAll(dateTime => dateTime.Hour == 12);

            return ret;
        }
    }
}
