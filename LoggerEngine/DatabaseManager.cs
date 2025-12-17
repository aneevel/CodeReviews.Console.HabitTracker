using Microsoft.Data.Sqlite;

namespace LoggerEngine
{
    internal class DatabaseManager
    {
        const int SEEDING_ROUNDS = 100;
        const int RANDOM_HABIT_AMOUNT_MAX = 100;
        
        SqliteConnection? connection;
        Random random = new();

        string[] habitChoices = [ "Walking", "Running", "Lift weights",
            "Clean room", "Make bed", "Do dishes", "Avoid sugary drinks"];

        public DatabaseManager(string connectionString)
        {
           connection = new SqliteConnection(connectionString);
           Initialize();
        }

        public void Close()
        {
            connection!.Close();
        }

        public void ViewHabits()
        {
            using (connection)
            {
                connection!.Open();
                using var command = connection!.CreateCommand();
                command.CommandText = "SELECT * FROM habits;";

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = reader.GetString(0);
                    var name = reader.GetString(1);
                    var date = reader.GetString(2);
                    var quantity = reader.GetString(3);

                    Console.WriteLine($"#{id} | {name} | {date} | {quantity}");
                }
                connection!.Close();
            }

        }

        public void InsertHabit(string name, DateOnly date, int quantity)
        {
            using (connection)
            {
                connection!.Open();
                using (SqliteCommand command = new SqliteCommand("INSERT INTO habits (Habit, Date, Quantity)" +
                    "VALUES (@name, @date, @quantity);", connection))
                {
                    command.Parameters.Add("@name", SqliteType.Text).Value = name;
                    command.Parameters.Add("@date", SqliteType.Text).Value = date;
                    command.Parameters.Add("@quantity", SqliteType.Integer).Value = quantity;

                    command.ExecuteNonQuery();
                }
                connection!.Close();
            }
        }

        public void UpdateHabit(int id, string name, DateOnly date, int quantity)
        {
            using (connection)
            {
                connection!.Open();

                using (SqliteCommand command = new SqliteCommand("UPDATE habits SET Habit = @name, " +
                    "Date = @date, Quantity = @quantity where id = @id", connection))
                {
                    command.Parameters.Add("@name", SqliteType.Text).Value = name;
                    command.Parameters.Add("@date", SqliteType.Text).Value = date;
                    command.Parameters.Add("@quantity", SqliteType.Integer).Value = quantity;
                    command.Parameters.Add("@id", SqliteType.Integer).Value = id;

                    command.ExecuteNonQuery();
                }
                connection!.Close();
            }
        }

        public void DeleteHabit(int id)
        {
            using (connection)
            {
                connection!.Open();

                using (SqliteCommand command = new SqliteCommand("DELETE FROM habits WHERE id = @id", connection))
                {
                    command.Parameters.Add("@id", SqliteType.Integer).Value = id;
                    command.ExecuteNonQuery();
                }
                connection!.Close();
            }
        }

        public bool HabitExists(int id)
        {
            using (connection)
            {
                connection!.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT id from habits WHERE id = @id", connection))
                {
                    command.Parameters.Add("@id", SqliteType.Integer).Value = id;
                    bool hasRows = command.ExecuteReader().HasRows;

                    connection.Close();
                    return hasRows;
                }
            }
        }

        void Initialize()
        {
            if (!HabitsTableExists())
            {
                CreateHabitsTable();
                SeedDatabase();
            }
        }
        
        void CreateHabitsTable()
        {
            using (connection)
            {
                connection!.Open();
                using var command = connection.CreateCommand();

                command.CommandText =
                    @"CREATE TABLE habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Habit TEXT,
                        Date TEXT,
                        Quantity INTEGER);";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        void SeedDatabase()
        {
            Console.WriteLine("Seeding database...");

            for (int i = 0; i < SEEDING_ROUNDS; i++)
            {
                string habit = habitChoices[random.Next(habitChoices.Length)];

                // Get some time in the past 15 years or so
                DateOnly start = new DateOnly(2010, 1, 1);
                int dayRange = DateOnly.FromDateTime(DateTime.Now).Day - start.Day;
                int monthRange = DateOnly.FromDateTime(DateTime.Now).Month - start.Month;
                int yearRange = DateOnly.FromDateTime(DateTime.Now).Year - start.Year;

                DateOnly date = start
                    .AddDays(random.Next(dayRange))
                    .AddMonths(random.Next(monthRange))
                    .AddYears(random.Next(yearRange));

                int amount = random.Next(RANDOM_HABIT_AMOUNT_MAX);

                InsertHabit(habit, date, amount);
            }
        }

        bool HabitsTableExists()
        {
            using (connection)
            {
                connection!.Open();

                using var command = connection.CreateCommand();
                command.CommandText =
                $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'habits'";

                var result = command.ExecuteScalar();
                connection!.Close();
                return result != null && (long)result > 0;
            }
        }
    }
}
