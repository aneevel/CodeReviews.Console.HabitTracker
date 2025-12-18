namespace LoggerEngine.Helpers
{
    /// <summary>
    /// Helper <c>DateOnlyHelper</c> provides utility methods for getting 
    /// validated DateOnly objects
    /// </summary>
    public class DateOnlyHelper
    {
        static Dictionary<Int32, Int32> DaysInMonth = new Dictionary<Int32, Int32>{
            { 1, 31 },
            { 2, 28 },
            { 3, 31 },
            { 4, 30 },
            { 5, 31 },
            { 6, 30 },
            { 7, 31 },
            { 8, 31 },
            { 9, 30 },
            { 10, 31 },
            { 11, 30 },
            { 12, 31 }
        };

        public static DateOnly Today = DateOnly.FromDateTime(DateTime.Now);

        /// <summary>
        /// Wrapper method around DateOnly.TryParse which checks valid
        /// month/day. Considers leap years as well.
        /// </summary>
        /// <param name="input">Input string to convert to DateOnly</param>
        /// <param name="date">DateOnly to store output in</param>
        /// <returns> true if the input is a valid DateOnly, false otherwise</returns>
        public static bool TryParse(string input, out DateOnly date)
        {
            if (!DateOnly.TryParse(input, out date))
                return false;

            if (!ValidMonth(date.Month))
                return false;

            if (!ValidDay(date))
                return false;

            return true;
        }

        /// <summary>
        /// Get current day
        /// </summary>
        /// <returns> DateOnly representing the current day</returns>
        public static DateOnly GetToday()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        
        /// <summary>
        /// Checks if given month is a valid one in a year 
        /// </summary>
        /// <param name="month">Month to check</param>
        /// <returns> true if input is a valid month, false otherwise</returns>
        public static bool ValidMonth(Int32 month)
        {
            return (month > 0 && month <= DaysInMonth.Count);
        }

        
        /// <summary>
        /// Checks if the given date has a valid day. Considers leap years in determination
        /// </summary>
        /// <param name="date">DateOnly to check</param>
        /// <returns>true if given date can exist within calendar, false otherwise</returns>
        public static bool ValidDay(DateOnly date)
        {
            Int32 day = date.Day;
            Int32 month = date.Month;
            Int32 year = date.Year;

            // Special case for leap year February check
            if (IsLeapYear(year) && month == 2)
                return day > 0 && day <= (DaysInMonth[2] + 1);

            // Otherwise, we can simply check against the month entry
            return day > 0 && day <= DaysInMonth[month];
        }

        /// <summary>
        /// Checks if the given year is a leap year
        /// </summary>
        /// <param name="year">Year to check</param>
        /// <returns>true if it is a leap year, false otherwise</returns>
        static bool IsLeapYear(int year)
        {
            return ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0));
        }

    }
}
