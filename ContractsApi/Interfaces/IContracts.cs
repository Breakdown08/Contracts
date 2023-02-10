using ContractsApi.Models;

namespace ContractsApi.Interfaces
{
    public interface IContracts
    {
        Task<IEnumerable<StudentModel>> GetStudentData();
        Task<IEnumerable<PayerModel>> GetPayerData();
        Task<IEnumerable<UniversityModel>> GetUniversityData();
        Task<IEnumerable<RequisitesModel>> GetRequisitesData();
        Task<IEnumerable<ContractModel>> GetContractData();
    }
}