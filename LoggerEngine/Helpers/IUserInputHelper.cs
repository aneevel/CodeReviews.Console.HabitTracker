using System.Text.RegularExpressions;

namespace LoggerEngine.Helpers
{
    /// <summary>
    /// Interface <c>IUserInputHelper</c> provides an interface for retrieving user input
    /// </summary>
    public interface IUserInputHelper
    {
        /// <summary>
        /// Get user input in the form of a string
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <returns>A string representing the user's input</returns>
        public string GetStringInput(string invalidInputMessage = "");

        /// <summary>
        /// Get user input in the form of an Int32
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <returns>An Int32 representing the user's input</returns>
        public Int32 GetInt32Input(string invalidInputMessage = "");

        /// <summary>
        /// Get user input in the form of DateOnly, provided it matches one of the provided RegEx
        /// </summary>
        /// <param name="expressions">An optional set of RegEx expressions, one of which the input must match</param>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param> 
        /// <returns>A DateOnly representing the user's input</returns>
        public DateOnly GetDateOnlyInputMatchingRegex(string invalidInputMessage = "", params Regex[] expressions);

    }
}
