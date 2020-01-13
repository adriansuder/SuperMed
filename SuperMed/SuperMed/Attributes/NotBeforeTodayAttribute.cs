using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Attributes
{
    public class NotBeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var today = DateTime.Today;

            return (DateTime) value >= today;
        }
    }
}