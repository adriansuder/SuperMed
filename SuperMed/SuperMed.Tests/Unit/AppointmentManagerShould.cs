using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SuperMed.Managers;

namespace SuperMed.Tests.Unit
{
    [TestFixture]
    public class AppointmentManagerShould
    {
        [Test]
        public void GenerateCorrectNumberOfTimes()
        {
            var availableDayTimes = AppointmentManager.GetAvailableTimes(DateTime.Today.AddDays(1));

            availableDayTimes.Count.Should().Be(28);
        }

        [Test]
        public void ReturnCorrectFirstHourOfDay()
        {
            var actual = AppointmentManager.GetAvailableTimes(DateTime.Today.AddDays(1));
            var firstTimeOfDay = actual.First().TimeOfDay;
            
            firstTimeOfDay.Should().Be(new TimeSpan(0, 8, 0, 0));
        }

        [Test]
        public void ReturnCorrectLastHourOfDay()
        {
            var actual = AppointmentManager.GetAvailableTimes(DateTime.Today.AddDays(1));
            var lastTimeOfDay = actual.Last().TimeOfDay;

            lastTimeOfDay.Should().Be(new TimeSpan(0, 15, 45, 0));
        }

        [Test]
        public void ReturnTimesEveryFifteenMinutes()
        {
            var actual = AppointmentManager.GetAvailableTimes(DateTime.Today.AddDays(1));
            var diffBetweenTimes = actual[1] - actual[0];

            diffBetweenTimes.Should().Be(new TimeSpan(0, 0, 15, 0));
        }

        [Test]
        public void NotReturnMidDayTimes()
        {
            var actual = AppointmentManager.GetAvailableTimes(DateTime.Today.AddDays(1));
            var hasAnyMidDayHour = actual.Any(t => t.Hour == 12);

            hasAnyMidDayHour.Should().BeFalse();
        }
    }
}
