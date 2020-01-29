using FluentAssertions;
using NUnit.Framework;
using SuperMed.Attributes;
using System;

namespace SuperMed.Tests.Unit
{
    [TestFixture]
    public class NotBeforeTodayAttributeShould
    {
        [Test]
        [Category("UnitTest")]
        public void ReturnTrueForToday()
        {
            var sut = new NotBeforeTodayAttribute();
            
            sut.IsValid(DateTime.Now).Should().BeTrue();
        }

        [Test]
        [Category("UnitTest")]
        public void ReturnFalseWhenYesterday()
        {
            var sut = new NotBeforeTodayAttribute();

            var yesterday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 0, 0, 0);

            sut.IsValid(yesterday).Should().BeFalse();
        }

        [Test]
        [Category("UnitTest")]
        public void ReturnFalseWhenTomorrow()
        {
            var sut = new NotBeforeTodayAttribute();

            var tomorrow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);

            sut.IsValid(tomorrow).Should().BeTrue();
        }
    }
}
