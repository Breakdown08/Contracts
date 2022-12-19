using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ContractsApi.Interfaces;
using ContractsApi.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;
namespace ContractsApi.Services
{
    public class DbService : IContracts
    {
        //private readonly ConnectionFactory _factory;
        //public DbService(ConnectionFactory factory)
        //{
        //    _factory = factory;
        //}

        public async Task<IEnumerable<StudentModel>> GetStudentData()
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source=db.sqlite");
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                connection.Open();
                return await connection.QueryAsync<StudentModel>(@"SELECT 
                    '123' LastName,
                    '244' FistName,
                    '4444' Patronymic,
                    'fdsfdssdffdssdf' Address,
                    'sdfsdf@inbox.ru' Email,
                    '8-906-922-12-24' PhoneNumber,
                    'asdasdasd' Passport,
                    'asdsdasaddsaasd' INN");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}