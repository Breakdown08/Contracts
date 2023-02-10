using Dapper;
using System.Data;
using ContractsApi.Interfaces;
using ContractsApi.Models;
namespace ContractsApi.Services
{
    public class SQLiteService : IContracts
    {
        private readonly ConnectionFactory _factory;
        public SQLiteService(ConnectionFactory factory)
        {
            _factory = factory;
        }


        public async Task<IEnumerable<ContractModel>> GetContractData()
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection("test"))
                {
                    var sql = @"SELECT 
                    'Иванов' LastName,
                    'Иван' FirstName,
                    'Николаевич' Patronymic,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 20-1' Address,
                    'example@gmail.com' Email,
                    '8-999-888-11-22' PhoneNumber,
                    'Паспорт гражданина России, серия 0122, номер 1234567, выдан в отделе выдачи паспортов по Алтайскому краю в городе Рубцовске' Passport,
                    '123456789123456789' INN";

                    var result = await dbcon.QueryAsync<ContractModel>(sql);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public async Task<IEnumerable<StudentModel>> GetStudentData()
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection("test"))
                {
                    var sql = @"SELECT 
                    'Иванов' LastName,
                    'Иван' FirstName,
                    'Николаевич' Patronymic,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 20-1' Address,
                    'example@gmail.com' Email,
                    '8-999-888-11-22' PhoneNumber,
                    'Паспорт гражданина России, серия 0122, номер 1234567, выдан в отделе выдачи паспортов по Алтайскому краю в городе Рубцовске' Passport,
                    '123456789123456789' INN";

                    var result = await dbcon.QueryAsync<StudentModel>(sql);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public async Task<IEnumerable<PayerModel>> GetPayerData()
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection("test"))
                {
                    var sql = @"SELECT 
                    'Иванов' LastName,
                    'Николай' FirstName,
                    'Михайлович' Patronymic,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 20-1' Address,
                    'example2@gmail.com' Email,
                    '8-999-777-33-44' PhoneNumber,
                    'Паспорт гражданина России, серия 0111, номер 7654321, выдан в отделе выдачи паспортов по Алтайскому краю в городе Рубцовске' Passport,
                    '987654321555555555' INN";


                    var result = await dbcon.QueryAsync<PayerModel>(sql);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<IEnumerable<RequisitesModel>> GetRequisitesData()
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection("test"))
                {
                    var sql = @"SELECT 
                    'Рубцовский институт (филиал) АлтГУ' ChildOrganizationName,
                    'Алтайский край, г. Рубцовск, пр-т. Ленина 200б' ChildAddress,
                    '(88888) 1-11-11' ChildPhone,
                    'example3.gmail.com' ChildEmail,
                    '(88888) 1-11-11' ChildFax,
                    '312232323123213213' ChildINN,
                    '31223232312' ChildKPP,
                    '96795679537' ChildLS,
                    'УФК по Алтайскому краю (Рубцовскийинститут (филиал) АлтГУ л/с 3232232323232)' ChildUFK,
                    '12323131223123123' ChildBIK,
                    '203402984308942309842390824' ChildKS,
                    '234823480923409243908234908' ChildEKS,
                    '4382984239' ChildOKVED,
                    '23442334' ChildOKPO,
                    '00000000000000000000111' ChildPurposePayment";

                    var result = await dbcon.QueryAsync<RequisitesModel>(sql);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public async Task<IEnumerable<UniversityModel>> GetUniversityData()
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection("test"))
                {
                    var sql = @"SELECT 
                    'Петров Петр Петрович' DirectorFullName,
                    'Петров П.П.' DirectorShortName,
                    'П.П. Петров' DirectorShortNameReverse";

                    var result = await dbcon.QueryAsync<UniversityModel>(sql);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}