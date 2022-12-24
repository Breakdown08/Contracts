using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net;
using ContractsApi.Models;
using ContractsApi.Interfaces;
using ContractsApi.Core;
using ContractsApi.BaseLibrary.Auth;

namespace ContractsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : Controller
    {
        private readonly IContracts _service;
        public ContractController(IContracts service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить путь до PDF-файла контракта
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("generate/")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContractModel))]
        [AuthResult("SuperAdmin")]
        public async Task<string> Get()
        {
            var contractData = new ContractModel();
            var studentData = (await _service.GetStudentData()).FirstOrDefault();
            var payerData = (await _service.GetPayerData()).FirstOrDefault();
            var requisitesData = (await _service.GetRequisitesData()).FirstOrDefault();
            var universityData = (await _service.GetUniversityData()).FirstOrDefault();
            
            universityData.Requisites = requisitesData;
            
            contractData.Student = studentData;
            contractData.Payer = payerData;
            contractData.University = universityData;
            
            var result = TemplateBuilder.GenerateDocument(contractData);
            return result;
        }
    }
}
