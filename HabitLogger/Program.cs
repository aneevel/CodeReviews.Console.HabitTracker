using LoggerEngine;
using LoggerEngine.Helpers;
using LoggerEngine.Database;

namespace HabitLogger
{
    class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=habit-tracker.db";

            Engine engine = new(new SqliteDatabaseManager(connectionString), new ConsoleUserInputHelper());
            engine.Run();
        }
    }
}
