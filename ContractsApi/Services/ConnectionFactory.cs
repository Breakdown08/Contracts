using System.Data;
using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;

namespace ContractsApi.Services
{
    public class ConnectionFactory
    {
        private readonly string _connectionString;
        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateOracleConnection(string connectionString) 
        {
            var connection = new OracleConnection(_connectionString);
            connection.Open();
            OracleGlobalization info = connection.GetSessionInfo();
            info.TimeZone = "Asia/Novosibirsk";
            connection.SetSessionInfo(info);
            return connection;
        }

        private IDbConnection CreateSQLiteConnection(string connectionString)
        {
            var connection = new SqliteConnection(_connectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            connection.Open();
            return connection;
        }

        public IDbConnection CreateConnection(string connectionMode = "production")
        {
            if (connectionMode == "production")
            {
                return CreateOracleConnection(_connectionString);
            }
            else
            {
                return CreateSQLiteConnection(_connectionString);
            }
        }
    }
}