using System.Text.RegularExpressions;

namespace LoggerEngine.Helpers
{
    public class ConsoleUserInputHelper : IUserInputHelper
    {
        /// <summary>
        /// Retrieves a string input from the user via Console
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <returns>A string representing the user's input</returns>
        public string GetStringInput(string invalidInputMessage = "")
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine($"{invalidInputMessage}");
                    continue;
                }
                return input!;
            }
        }

        /// <summary>
        /// Retrieves an Int32 input from the user via Console
        /// </summary>
        /// <param name="invalidInputMessage">An optional message for when user provides an invalid input</param>
        /// <returns>An Int32 representing the user's input</returns>
        public Int32 GetInt32Input(string invalidInputMessage = "")
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (Int32.TryParse(input, out Int32 output))
                    return output;
                Console.WriteLine($"{invalidInputMessage}");
            }
        }

        public DateOnly GetDateOnlyInputMatchingRegex(string invalidInputMessage = "", params Regex[] expressions)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (input != null)
                {
                    // Shortcut for no regex to match
                    if (expressions == null)
                    {
                       if (DateOnly.TryParse(input, out DateOnly dateOnly))
                            return dateOnly;
                       
                    }
                    else
                    {
                        foreach (Regex regex in expressions)
                        {
                            if (Regex.IsMatch(input, regex.ToString()))
                            {
                                if (DateOnly.TryParse(input, out DateOnly dateOnly))
                                    return dateOnly;
                            }
                        }
                    }
                }
                Console.WriteLine($"{invalidInputMessage}");
            }
        }
    }
}
