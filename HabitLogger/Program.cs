using LoggerEngine;
using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    class Program
    {
        static SqliteConnection? connection;

        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=habit-tracker.db";

            Engine engine = new(connectionString);
            engine.Run();
        }
    }
}
