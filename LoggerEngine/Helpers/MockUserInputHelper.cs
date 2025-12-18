using System.Text.RegularExpressions;

namespace LoggerEngine.Helpers
{
    /// <summary>
    /// Provides a concrete implementation of <c>IUserInputHelper</c> used for mocking.
    /// </summary>
    public class MockUserInputHelper : IUserInputHelper
    {
        /// <summary>
        /// Mock get user input in the form of a string
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <param name="acceptEmpty">An optional param for accepting empty input</param>
        /// <returns>string "input"</returns>
        public string GetStringInput(string invalidInputMessage = "", bool acceptEmpty = false)
        {
            return "input";
        }

        /// <summary>
        /// Mock get user input in the form of an Int32
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <returns>Int32 1</returns>
        public Int32 GetInt32Input(string invalidInputMessage = "")
        {
            return 1;
        }

        /// <summary>
        /// Mock get user input in the form of DateOnly, provided it matches one of the provided RegEx
        /// </summary>
        /// <param name="expressions">An optional set of RegEx expressions, one of which the input must match</param>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param> 
        /// <returns>DateOnly for current day</returns>
        public DateOnly GetDateOnlyInputMatchingRegex(string invalidInputMessage = "", params Regex[] expressions)
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
