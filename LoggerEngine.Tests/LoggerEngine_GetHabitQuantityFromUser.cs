using LoggerEngine;
using LoggerEngine.Helpers;

namespace LoggerEngine.Tests
{
    [TestClass]
    public class LoggerEngine_GetHabitQuantityFromUser
    {
        private readonly Engine _loggerEngine;

        public LoggerEngine_GetHabitQuantityFromUser()
        {
            _loggerEngine = new Engine("", new MockUserInputHelper());
        }

        [TestMethod]
        public void GetHabitQuantityFromUser_ShouldReturnAnInt32()
        {
            var result = _loggerEngine.GetHabitQuantityFromUser();

            Assert.IsInstanceOfType(result, typeof(Int32), "GetHabitQuantityFromUser did not return an Int32!");
        }

        [TestMethod]
        public void GetHabitNameFromUser_ShouldReturnAString()
        {
            var result = _loggerEngine.GetHabitNameFromUser();

            Assert.IsInstanceOfType(result, typeof(string), "GetHabitNameFromUser did not return a string");
            Assert.IsGreaterThan(0, result.Length, "GetHabitNameFromUser did not return a non-empty string!");
        }

        [TestMethod]
        public void GetHabitDateFromUser_ShouldReturnADateOnly()
        {
            var result = _loggerEngine.GetHabitDateFromUser();

            Assert.IsInstanceOfType(result, typeof(DateOnly));
        }
    }
}
