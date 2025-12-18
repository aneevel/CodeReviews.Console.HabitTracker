
namespace LoggerEngine.Database
{
    public class MockDatabaseManager : IDatabaseManager
    {
        /// <summary>
        /// Mock method for reading records; always returns
        /// </summary>
        public void ReadRecords()
        {
            return;
        }

        /// <summary>
        /// Mock method for inserting records; always returns
        /// </summary>
        /// <param name="name">Name of record</param>
        /// <param name="date">Date of record</param>
        /// <param name="quantity">Quantity of record</param>
        public void InsertRecord(string name, DateOnly date, int quantity)
        {
            return;
        }

        /// <summary>
        /// Mock method for updating records; always returns
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <param name="name">Name of record</param>
        /// <param name="date">Date of record</param>
        /// <param name="quantity">Quantity of record</param>
        public void UpdateRecord(int id, string name, DateOnly date, int quantity)
        {
            return;
        }

        /// <summary>
        /// Mock method for deleting records; always returns
        /// </summary>
        /// <param name="id">ID of record</param>
        public void DeleteRecord(int id)
        {
            return;
        }

        /// <summary>
        /// Mock method for checking if record exists; always returns true
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>true </returns>
        public bool RecordExists(int id)
        {
            return true;
        }

        /// <summary>
        /// Mock method for checking if table exists; always returns true
        /// </summary>
        /// <returns></returns>
        public bool TableExists()
        {
            return true;
        }
    }
}
