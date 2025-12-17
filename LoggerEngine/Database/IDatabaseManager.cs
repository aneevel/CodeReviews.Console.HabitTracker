using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LoggerEngine.Database
{
    /// <summary>
    /// Interface <c>IDatabaseManager</c> provides an interface for an object which handles database operations 
    /// </summary>
    public interface IDatabaseManager
    {
        /// <summary>
        /// Close the database connection
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Read the records from given table
        /// </summary>
        /// <param name="tableName">Name of the table to read from</param>
        public abstract void ReadRecords(string tableName);

        /// <summary>
        /// Insert a record with the passed parameters
        /// </summary>
        /// <param name="tableName">Name of the table to insert record into</param>
        /// <param name="name">Name of the record</param>
        /// <param name="date">Date of the record</param>
        /// <param name="quantity">Quantity of the record</param>

        public abstract void InsertRecord(string tableName, string name, DateOnly date, int quantity);

        /// <summary>
        /// Update a record with the given ID
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="id">ID of the record</param>
        /// <param name="name">Name of the record</param>
        /// <param name="date">Date of the record</param>
        /// <param name="quantity">Quantity of the record</param>
        public abstract void UpdateRecord(string tableName, int id, string name, DateOnly date, int quantity);

        /// <summary>
        /// Delete a record with the given ID
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="id">ID of the record</param>
        public abstract void DeleteRecord(string tableName, int id);

        /// <summary>
        /// Check if a record exists with given ID
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="id">Id of the record</param>
        public abstract bool RecordExists(string tableName, int id);

        /// <summary>
        /// Check if a table exists with given name
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <returns></returns>
        public abstract bool TableExists(string tableName);
    }
}
