using FluentAssertions;
using NUnit.Framework;
using SuperMed.Extensions;

namespace SuperMed.Tests.Unit
{
    [TestFixture]
    public class StringExtensionShould
    {
        [Test]
        [Category("UnitTest")]
        [TestCase("a","A")]
        [TestCase("aa", "Aa")]
        [TestCase("tEst", "Test")]
        [TestCase("aaa-bbb", "Aaa-Bbb")]
        [TestCase("aAa-bBb", "Aaa-Bbb")]
        [TestCase("aAa-bBb", "Aaa-Bbb")]
        [TestCase("aaa bbb", "Aaa Bbb")]
        [TestCase("aAa bBb", "Aaa Bbb")]
        [TestCase("aAa bBb", "Aaa Bbb")]
        [TestCase(" aaa", "Aaa")]
        [TestCase("aaa ", "Aaa")]
        [TestCase("Aaa-Bbb ", "Aaa-Bbb")]
        [TestCase("Aaa Bbb ", "Aaa Bbb")]
        [TestCase("Aaa Bbb-Ccc ", "Aaa Bbb-Ccc")]
        [TestCase("aaa bbb-ccc ", "Aaa Bbb-Ccc")]
        [TestCase("aaa-bbb ccc ", "Aaa-Bbb Ccc")]
        public void ReturnCorrectCapitalizedValues(string input, string expected)
        {
            var actual = input.Capitalize();

            actual.Should().Be(expected);
        }
    }
}
