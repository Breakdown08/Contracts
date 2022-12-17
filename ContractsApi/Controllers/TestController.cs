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
        [HttpGet(Name = "GetTestStudent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentModel))]
        public async Task<IActionResult> GetAsync()
        {
            var items = await _service.GetStudentData();
            if (items != null)
            {
                return (IActionResult)Results.Ok(items);
            }

            return NotFound();
        }
    }
}