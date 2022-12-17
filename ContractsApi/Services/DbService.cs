using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ContractsApi.Interfaces;
using ContractsApi.Models;

namespace ContractsApi.Services
{
    public class DbService : IContracts
    {
        private readonly ConnectionFactory _factory;
        public DbService(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentData()
        {
            try
            {
                using IDbConnection dbcon = _factory.CreateConnection();
                var items =
                    @"SELECT 
                    '123' LastName,
                    '244' FistName,
                    '4444' Patronymic,
                    'fdsfdssdffdssdf' Address,
                    'sdfsdf@inbox.ru' Email,
                    '8-906-922-12-24' PhoneNumber,
                    'asdasdasd' Passport,
                    'asdsdasaddsaasd' INN";
                return (IEnumerable<StudentModel>)await dbcon.QueryFirstAsync<StudentModel>(items);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}