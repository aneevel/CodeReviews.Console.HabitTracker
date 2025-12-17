using System.Text.RegularExpressions;
using LoggerEngine.Helpers;
using LoggerEngine.Database;

namespace LoggerEngine
{
    public class Engine
    {
        static readonly Dictionary<Int32, string> menuOptions = new Dictionary<Int32, string>
        {
            { 0, "CLOSE APPLICATION" },
            { 1, "VIEW ALL HABITS" },
            { 2, "INSERT NEW HABIT" },
            { 3, "UPDATE EXISTING HABIT" },
            { 4, "DELETE EXISTING HABIT" }
        };

        IDatabaseManager databaseManager;
        IUserInputHelper userInputHelper;

        public Engine(IDatabaseManager databaseManager, IUserInputHelper userInputHelper)
        {
            this.databaseManager = databaseManager;
            this.userInputHelper = userInputHelper;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Habit Logger, C# edition!");
            Console.WriteLine("-----------------------------------\n");

            while (true)
            {
                Console.WriteLine("MAIN MENU\n");
                Console.WriteLine("Please choose an option:\n");

                foreach (var menuOption in Engine.menuOptions)
                {
                    Console.WriteLine($"{menuOption.Key} - {menuOption.Value}");
                }
                Console.WriteLine("--------------------------------\n");

                Int32 option = userInputHelper.GetInt32Input("Please enter a numeric value corresponding to a menu option.");

                if (!(0 <= option && option < Engine.menuOptions.Count))
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

                databaseManager.ReadRecords();

                Console.WriteLine("Enter any key to return to main menu...\n");
                _ = userInputHelper.GetStringInput("", true);

                break;
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

                databaseManager.InsertRecord(name, date, quantity);

                Console.WriteLine("Habit added!");
                return;
            }
        }

        void HandleUpdateHabit() 
        { 
            while (true)
            {
                Console.WriteLine("Enter the id number of the habit you wish to update:");

                Int32 updateId = userInputHelper.GetInt32Input("Please enter a numeric value for quantity.");

                if (databaseManager.RecordExists(updateId))
                {
                    string name = GetHabitNameFromUser();
                    DateOnly date = GetHabitDateFromUser();
                    int quantity = GetHabitQuantityFromUser();

                    databaseManager.UpdateRecord(updateId, name, date, quantity);

                    Console.WriteLine($"Habit with id {updateId} updated!");

                    return;
                }
                else
                {
                    Console.WriteLine($"Habit with id {updateId} does not exist; please check your entries.");
                    continue;
                }
            }
        }

        void HandleDeleteHabit() 
        { 
            while (true)
            {
                Console.WriteLine("Enter the id number of the habit you wish to delete:");

                Int32 deleteId = userInputHelper.GetInt32Input("Please enter a numeric value for quantity.");

                if ( databaseManager.RecordExists(deleteId))
                {
                    databaseManager.DeleteRecord(deleteId);

                    Console.WriteLine($"Habit {deleteId} deleted!");
                    return;
                }
                else
                {
                    Console.WriteLine($"Habit with id {deleteId} does not exist; please check your entries.");
                    continue;
                }
            }
        }

        public int GetHabitQuantityFromUser()
        {
            while (true)
            {
                Console.WriteLine("Quantity of habit:");

                return userInputHelper.GetInt32Input("Please enter a numeric value for quantity.");
            }
        }

        public DateOnly GetHabitDateFromUser()
        {
            while (true)
            {
                Console.WriteLine("Date of habit (YYYY-MM-DD) or (t) for current date:");

                return userInputHelper.GetDateOnlyInputMatchingRegex("Please enter a date in the correct format, or (t) for current date:",
                    new Regex("^\\d{4}[-]\\d{2}[-]\\d{2}$"), 
                    new Regex("^t$"));
            }
        }

        public string GetHabitNameFromUser()
        {
            while (true)
            {
                Console.WriteLine("Enter name of the habit:");

                return userInputHelper.GetStringInput("Please enter a valid text name.");
            }
        }
    }
}
