using LoggerEngine;
using LoggerEngine.Helpers;

namespace HabitLogger
{
    class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=habit-tracker.db";

            Engine engine = new(connectionString, new ConsoleUserInputHelper());
            engine.Run();
        }
    }
}
