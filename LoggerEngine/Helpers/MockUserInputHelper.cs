using System.Text.RegularExpressions;

namespace LoggerEngine.Helpers
{
    public class MockUserInputHelper : IUserInputHelper
    {
        public string GetStringInput(string invalidInputMessage = "", bool acceptEmpty = false)
        {
            return "input";
        }

        public Int32 GetInt32Input(string invalidInputMessage = "")
        {
            return 1;
        }

        public DateOnly GetDateOnlyInputMatchingRegex(string invalidInputMessage = "", params Regex[] expressions)
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
