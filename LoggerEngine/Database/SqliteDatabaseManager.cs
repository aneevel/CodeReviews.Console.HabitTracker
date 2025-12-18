using Microsoft.Data.Sqlite;

namespace LoggerEngine.Database
{
    /// <summary>
    /// Concrete implementation of <c>IDatabaseManager</c> that can interface with Sqlite databases.
    /// Contains helper methods for initializing and populating database
    /// </summary>
    public class SqliteDatabaseManager : IDatabaseManager
    {
        // How many records to seed empty database with
        const int SEEDING_ROUNDS = 100;

        // Maximum quantity to use in random seeds
        const int RANDOM_HABIT_AMOUNT_MAX = 100;
        
        SqliteConnection? connection;
        Random random = new();

        // Stock habit choices to use in random generation
        string[] habitChoices = [ "Walking", "Running", "Lift weights",
            "Clean room", "Make bed", "Do dishes", "Avoid sugary drinks"];

        /// <summary>
        /// Constructs a new <c>SqliteDatabaseManager</c> and connects to database with given name.
        /// Creates one if it doesn't exist.
        /// </summary>
        /// <param name="connectionString">Name of the database file to connect to</param>
        public SqliteDatabaseManager(string connectionString)
        {
           connection = new SqliteConnection(connectionString);
           Initialize();
        }

        /// <summary>
        /// Read the records from main table
        /// </summary>
        public void ReadRecords()
        {
            using (connection)
            {
                connection!.Open();
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM habits;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetString(0);
                            var name = reader.GetString(1);
                            var date = reader.GetString(2);
                            var quantity = reader.GetString(3);

                            Console.WriteLine($"#{id} | {name} | {date} | {quantity}");
                        }
                    }
                }
            }
            connection!.Close();

        }

        /// <summary>
        /// Insert a record with the passed parameters
        /// </summary>
        /// <param name="name">Name of the record</param>
        /// <param name="date">Date of the record</param>
        /// <param name="quantity">Quantity of the record</param>
        public void InsertRecord(string name, DateOnly date, int quantity)
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


        /// <summary>
        /// Update a record with the given ID
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <param name="name">Name of the record</param>
        /// <param name="date">Date of the record</param>
        /// <param name="quantity">Quantity of the record</param>
        public void UpdateRecord(int id, string name, DateOnly date, int quantity)
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

                    command.ExecuteScalar();
                }
                connection!.Close();
            }
        }

        /// <summary>
        /// Delete a record with the given ID
        /// </summary>
        /// <param name="id">ID of the record</param>
        public void DeleteRecord(int id)
        {
            using (connection)
            {
                connection!.Open();

                using (SqliteCommand command = new SqliteCommand("DELETE FROM habits WHERE id = @id", connection))
                {
                    command.Parameters.Add("@id", SqliteType.Integer).Value = id;
                    command.ExecuteScalar();
                }
                connection!.Close();
            }
        }

        /// <summary>
        /// Check if a record exists with given ID
        /// </summary>
        /// <param name="id">Id of the record</param>
        public bool RecordExists(int id)
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

        /// <summary>
        /// Creates and seeds database if no data exists
        /// </summary>
        void Initialize()
        {
            if (!TableExists())
            {
                CreateTable();
                SeedDatabase();
            }
        }
        
        /// <summary>
        /// Create the base table, which is assumed to be habits
        /// </summary>
        void CreateTable()
        {
            using (connection)
            {
                connection!.Open();
                using var command = connection.CreateCommand();

                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Habit TEXT,
                        Date TEXT,
                        Quantity INTEGER);";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Seeds the database with semi-randomized data. Useful if no data is initially present
        /// </summary>
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

                InsertRecord(habit, date, amount);
            }
        }


        /// <summary>
        /// Check if the main table in database exists
        /// </summary>
        /// <returns> true if it exists, false otherwise </returns>
        public bool TableExists()
        {
            using (connection)
            {
                connection!.Open();

                using (SqliteCommand command = new SqliteCommand($"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'habits'",
                     connection))
                {

                    var result = command.ExecuteScalar();
                    connection.Close();
                    return result != null && (long)result > 0;
                }
            }
        }
    }
}
