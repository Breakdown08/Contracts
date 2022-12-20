using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net;
//using WebCore.ApiBase.Infrastructure.ActionResult;
using ContractsApi.Models;
using ContractsApi.Interfaces;
using ContractsApi.Services;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.IO.Packaging;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO.Compression;
using HtmlToOpenXml;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using ContractsApi.Core;

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
        public async Task<ContractModel> Get()
        {
            var contractData = new ContractModel();
            var studentData = await _service.GetStudentData();
            contractData.Student = studentData.FirstOrDefault();
            var result = TemplateBuilder.GenerateDocument(contractData);
            return result;
        }
    } 
}
