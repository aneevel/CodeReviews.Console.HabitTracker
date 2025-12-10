namespace HabitLogger
{
    class Program
    {
        public static void Main(string[] args)
        {
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
            }
        }
    }
}