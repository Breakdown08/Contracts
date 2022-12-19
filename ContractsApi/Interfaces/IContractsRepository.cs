using System.Collections.Generic;
using System.Threading.Tasks;
using ContractsApi.Models;

namespace ContractsApi.Interfaces
{
    public interface IContractsRepository
    {
        Task Create(StudentModel student);
    }
}
