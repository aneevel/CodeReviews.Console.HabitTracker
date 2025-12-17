using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;

namespace LoggerEngine
{
    public class Engine
    {
        DatabaseManager databaseManager;

        public Engine(string connectionString)
        {
            databaseManager = new(connectionString);
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Habit Logger, C# edition!");
            Console.WriteLine("-----------------------------------\n");

            while (true)
            {
                Console.WriteLine("MAIN MENU\n");
                Console.WriteLine("Please choose an option:\n");
                Console.WriteLine(
                    "0 - Close Application\n1 - View all Habits\n2 - Insert a new Habit\n3 - Update an existing Habit\n4 - Delete an existing Habit"
                );
                Console.WriteLine("--------------------------------\n");

                string? input = Console.ReadLine();

                // TODO: Replace magic variables
                if (!Int32.TryParse(input, out int option) || !(0 <= option && option <= 5))
                {
                    Console.WriteLine(
                        "Invalid option chosen. Please choose one of the provided options."
                    );
                    continue;
                }

                switch (option)
                {
                    case 0:
                        databaseManager.Close();
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

        void HandleViewHabits()
        {
            while (true)
            {
                Console.WriteLine("Displaying all habits");
                Console.WriteLine("---------------------");

                databaseManager.ViewHabits();

                Console.WriteLine("Enter any key to return to main menu...\n");
                string? input = Console.ReadLine();

                if (input != null)
                {
                    break;
                }
            }
        }

        void HandleInsertHabit()
        {
            while (true)
            {
                Console.WriteLine("Please input the habit parameters;");

                string name = GetHabitNameFromUser();
                DateOnly date = GetHabitDateFromUser();
                int quantity = GetHabitQuantityFromUser();

                databaseManager.InsertHabit(name, date, quantity);

                Console.WriteLine("Habit added!");
                return;
            }
        }

        void HandleUpdateHabit() 
        { 
            while (true)
            {
                Console.WriteLine("Enter the id number of the habit you wish to update:");

                string? input = Console.ReadLine();

                if (Int32.TryParse(input, out int updateId))
                {
                    if (databaseManager.HabitExists(updateId))
                    {
                        string name = GetHabitNameFromUser();
                        DateOnly date = GetHabitDateFromUser();
                        int quantity = GetHabitQuantityFromUser();

                        databaseManager.UpdateHabit(updateId, name, date, quantity);

                        Console.WriteLine($"Habit with id {updateId} updated!");

                        return;
                    }
                    Console.WriteLine($"Habit with id {updateId} does not exist; please check your entries.");
                    continue;
                }
                Console.WriteLine("Please enter a valid number id.");
            }
        }

        void HandleDeleteHabit() 
        { 
            while (true)
            {
                Console.WriteLine("Enter the id number of the habit you wish to delete:");

                string? input = Console.ReadLine(); 

                if (Int32.TryParse (input, out int deleteId))
                {
                    if ( databaseManager.HabitExists(deleteId))
                    {
                        databaseManager.DeleteHabit(deleteId);

                        Console.WriteLine($"Habit {deleteId} deleted!");
                        return;
                    }
                    Console.WriteLine($"Habit with id {deleteId} does not exist; please check your entries.");
                    continue;
                }
                Console.WriteLine("Please enter a valid number id.");
            }
        }

        int GetHabitQuantityFromUser()
        {
            int quantityConverted;
            while (true)
            {
                Console.WriteLine("Quantity of habit:");

                string? input = Console.ReadLine();
                if (Int32.TryParse(input, out quantityConverted))
                    return quantityConverted;

                Console.WriteLine("Please enter a numeric value for quantity.");
            }
        }

        DateOnly GetHabitDateFromUser()
        {
            DateOnly date;
            while (true)
            {
                Console.WriteLine("Date of habit (YYYY-MM-DD) or (t) for current date:");

                string? input = Console.ReadLine();
                if (input != null && Regex.IsMatch(input, "^\\d{4}[-]\\d{2}[-]\\d{2}$"))
                {
                    DateOnly.TryParse(input, out date);
                    return date;
                }
                else if (input != null && Regex.IsMatch(input, "^t$"))
                {
                    return DateOnly.FromDateTime(DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Please enter a date in the correct format, or (t) for current date:");
                }
            }
        }

        string GetHabitNameFromUser()
        {
            string? input;
            while (true)
            {
                Console.WriteLine("Enter name of the habit:");

                input = Console.ReadLine();
                if (input != null)
                {
                    return input;
                }
                Console.WriteLine("Please enter a valid text name.");
            }
        }
    }
}
