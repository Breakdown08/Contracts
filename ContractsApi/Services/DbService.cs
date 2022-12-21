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
                    'Иванов' LastName,
                    'Иван' FirstName,
                    'Николаевич' Patronymic,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 20-1' Address,
                    'example@gmail.com' Email,
                    '8-999-888-11-22' PhoneNumber,
                    'Паспорт гражданина России, серия 0122, номер 1234567, выдан в отделе выдачи паспортов по Алтайскому краю в городе Рубцовске' Passport,
                    '123456789123456789' INN");
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<IEnumerable<PayerModel>> GetPayerData()
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source=db.sqlite");
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                connection.Open();
                return await connection.QueryAsync<PayerModel>(@"SELECT 
                    'Иванов' LastName,
                    'Николай' FirstName,
                    'Михайлович' Patronymic,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 20-1' Address,
                    'example2@gmail.com' Email,
                    '8-999-777-33-44' PhoneNumber,
                    'Паспорт гражданина России, серия 0111, номер 7654321, выдан в отделе выдачи паспортов по Алтайскому краю в городе Рубцовске' Passport,
                    '987654321555555555' INN");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<RequisitesModel>> GetRequisitesData()
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source=db.sqlite");
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                connection.Open();
                return await connection.QueryAsync<RequisitesModel>(@"SELECT 
                    'Рубцовский институт (филиал) АлтГУ' ChildOrganizationName,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 200б' ChildAddress,
                    '(88888) 1-11-11' ChildPhone,
                    'example3.gmail.com' ChildEmail,
                    '(88888) 1-11-11' ChildFax,
                    '312232323123213213' ChildINN,
                    'УФК по Алтайскому краю (Рубцовскийинститут (филиал) АлтГУ л/с 3232232323232)' ChildUFK,
                    '12323131223123123' ChildBIK,
                    '203402984308942309842390824' ChildKS,
                    '234823480923409243908234908' ChildEKS,
                    '4382984239' ChildOKVED,
                    '23442334' ChildOKPO,
                    '00000000000000000000111' ChildPurposePayment
                    ");
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<IEnumerable<UniversityModel>> GetUniversityData()
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source=db.sqlite");
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                connection.Open();
                return await connection.QueryAsync<UniversityModel>(@"SELECT 
                    'Петров Петр Петрович' DirectorFullName,
                    'Петров П.П.' DirectorShortName,
                    'П.П. Петров' DirectorShortNameReverse
                    ");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}