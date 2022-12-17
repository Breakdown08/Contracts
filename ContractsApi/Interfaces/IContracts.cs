using System.Collections.Generic;
using System.Threading.Tasks;
using ContractsApi.Models;

namespace ContractsApi.Interfaces
{
    public interface IContracts
    {
        Task<IEnumerable<StudentModel>> GetStudentData();
    }
}
