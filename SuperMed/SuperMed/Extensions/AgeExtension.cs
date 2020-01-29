using System;

namespace SuperMed.Extensions
{
    public static class AgeExtension
    {
        public static int GetAge(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var age = now.Year - dateTime.Year;

            if (DateTime.Now.Year < dateTime.Year)
                age -= 1;

            return age;
        }
    }
}
