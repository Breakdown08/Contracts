using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ContractsApi.Services
{
    public class ConnectionFactory
    {
        private readonly string _connectionString;
        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}

//public void Setup()
//{
//    using var connection = new SqliteConnection(databaseConfig.Name);

//    var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
//    var tableName = table.FirstOrDefault();
//    if (!string.IsNullOrEmpty(tableName) && tableName == "Product")
//        return;

//    connection.Execute("Create Table Product (" +
//        "Name VARCHAR(100) NOT NULL," +
//        "Description VARCHAR(1000) NULL);");
//}
