using FluentAssertions;
using NUnit.Framework;
using SuperMed.Extensions;
using System;

namespace SuperMed.Tests.Unit
{
    [TestFixture]
    public class AgeExtensionShould
    {
        [Test]
        [Category("UnitTest")]
        public void ReturnCorrectValue()
        {
            var dt = new DateTime(2000, 01, 01, 0, 0, 0);

            var sut = dt.GetAge();

            sut.Should().Be(20);
        }
    }
}
