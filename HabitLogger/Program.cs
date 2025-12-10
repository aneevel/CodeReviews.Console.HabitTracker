using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    class Program
    {
        static SqliteConnection? connection;
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=habit-tracker.db";

            using (connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Habit TEXT,
                        Date TEXT,
                        Quantity INTEGER)";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            Console.WriteLine("Welcome to Habit Logger, C# edition!");
            Console.WriteLine("-----------------------------------\n");

            while (true)
            {
                Console.WriteLine("MAIN MENU\n");
                Console.WriteLine("Please choose an option:\n");
                Console.WriteLine("0 - Close Application\n1 - View all Habits\n2 - Insert a new Habit\n3 - Update an existing Habit\n4 - Delete an existing Habit");
                Console.WriteLine("--------------------------------\n");

                string? input = Console.ReadLine();
                if (!Int32.TryParse(input, out int option) || !(0 <= option && option <= 5))
                {
                    Console.WriteLine("Invalid option chosen. Please choose one of the provided options.");
                    continue;
                }

                switch (option)
                {
                    case 0:
                        connection.Close();
                        Environment.Exit(0);
                        break;
                    case 1:
                        HandleViewHabits();
                        break;
                    case 2:
                        HandleInsertHabit();
                        break;
                    case 3:
                        HandleUpdateHabit();
                        break;
                    case 4:
                        HandleDeleteHabit();
                        break;
                }
            }
        }

        static void HandleViewHabits()
        {
            while (true)
            {
                Console.WriteLine("Displaying all habits");
                Console.WriteLine("---------------------");

                Console.WriteLine("Press any key to return to main menu...");
                string? input = Console.ReadLine();

                if (input != null)
                {
                    break;
                }
            }
        }

        static void HandleInsertHabit()
        {
            while (true)
            {
                Console.WriteLine("Please input the habit parameters;");
                Console.WriteLine("Name of habit:");

                string? name = Console.ReadLine();

                Console.WriteLine("Date of habit (YYYY-MM-DD):");

                string? date = Console.ReadLine();

                int quantityConverted;
                while (true)
                {
                    Console.WriteLine("Quantity of habit:");

                    string? quantityString = Console.ReadLine();
                    if (Int32.TryParse(quantityString, out quantityConverted))
                        break;
                    
                    Console.WriteLine("Please enter a numeric value for quantity.");
                }

                connection!.Open();
                using var command = connection!.CreateCommand();
                command.CommandText = $"INSERT INTO habits " +
                    $"(Habit, Date, Quantity) VALUES ('{name}', '{date}', {quantityConverted});";
                command.ExecuteNonQuery();
                return;
            }
        }

        static void HandleUpdateHabit()
        {

        }

        static void HandleDeleteHabit()
        {

        }
    }
}