using ContractsApi.BaseLibrary.Auth;
using ContractsApi.Interfaces;
using ContractsApi.Models;
using ContractsApi.Services;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace ContractsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[AuthResult("Admin")]
    public class TestController : ControllerBase
    {
        private readonly IContracts _service;
        public TestController(IContracts service)
        {
            _service = service;
        }
        [HttpGet(Name = "GetTest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UniversityModel))]
        public async Task<IEnumerable<UniversityModel>> GetAsync()
        {
            return await _service.GetUniversityData();
        }
    }
}