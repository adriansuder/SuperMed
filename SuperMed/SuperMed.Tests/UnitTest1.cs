using System;
using NUnit.Framework;
using SuperMed.Managers;

namespace SuperMed.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            AppointmentManager manager = new AppointmentManager();
            manager.GenerateDateTimes(DateTime.Now);
            var nearest = manager.GetNearest(DateTime.Now);
        }
    }
}