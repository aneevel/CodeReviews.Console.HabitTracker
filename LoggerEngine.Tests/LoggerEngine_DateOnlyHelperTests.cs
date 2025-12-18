using LoggerEngine.Helpers;

namespace LoggerEngine.Tests
{
    [TestClass]
    public class LoggerEngine_DateOnlyHelperTests
    {

        [DataRow("2020-01-31")]
        [DataRow("2020-01-01")]
        [DataRow("2000-02-29")]
        [TestMethod, Description("Ensures TryParse returns a true value and DateOnly object given valid input")]
        public void GivenValidDateString_TryParse_ShouldReturnADateOnly(string input)
        {
            var result = DateOnlyHelper.TryParse(input, out DateOnly dateOnly);

            Assert.IsTrue(result);
            Assert.AreNotEqual(dateOnly, DateOnly.MinValue);
        }

        [DataRow("2020-01-32")]
        [DataRow("2020-02-31")]
        [DataRow("2001-02-29")]
        [DataRow("20020-01-01")]
        [DataRow("2020-011-01")]
        [DataRow("2020-01-011")]
        [DataRow("Test")]
        [TestMethod, Description("Ensures TryParse returns a false value and DateOnly.MinValue given invalid input")]
        public void GivenInvalidDateString_TryParse_ShouldReturnFalse(string input)
        {
            var result = DateOnlyHelper.TryParse(input, out DateOnly dateOnly);

            Assert.IsFalse(result);
            Assert.AreEqual(dateOnly, DateOnly.MinValue);
        }
    }
}
