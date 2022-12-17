using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net;
//using WebCore.ApiBase.Infrastructure.ActionResult;
using ContractsApi.Models;
using ContractsApi.Interfaces;
using ContractsApi.Services;
using System.Linq;
using io = System.IO;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateFileModel))]
        public async Task<IActionResult> Get()
        {

            io.File.Copy//(@"/srv/api/apiworkprogram/docs/pattern.docx", @"/srv/api/apiworkprogram/docs/patternCopy.docx", true);
            (@"C:\Users\Kirill\Desktop\pattern.docx", @"C:\Users\Kirill\Desktop\patternCopy.docx", true);
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open//(@"/srv/api/apiworkprogram/docs/patternCopy.docx", true))
                    (@"C:\Users\Kirill\Desktop\patternCopy.docx", true))
                {
                    HtmlConverter converter = new HtmlConverter(wordDoc.MainDocumentPart);
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    var studentData = _service.GetStudentData().Result;


                    var body = wordDoc.MainDocumentPart.Document.Body;
                    ChangeBookmark(body, "ФамилияСтудента", studentData.FirstOrDefault().FistName);
                    ChangeBookmark(body, "ИмяСтудента", studentData.FirstOrDefault().FistName);
                }

                //Close the handle explicitly.
                //DMSDocToPdfMain//(@"/srv/api/apiworkprogram/docs/patternCopy.docx", @"/srv/api/apiworkprogram/docs/");
                //(@"C:\Users\Kirill\Desktop\patternCopy.docx", @"C:\Users\Kirill\Desktop");
                var model = new CreateFileModel();
                model.filePath = @"/srv/api/apiworkprogram/myPDFDocument";
                //return new JsonResult(new SuccessResult<CreateFileModel>(model));
                return (IActionResult)Results.Ok(model);

            }
            catch (Exception e)
            {
                return BadRequest(error: e);
            }
        }
        public static void DMSDocToPdfMain(string Path, string EnvironmentDirectoryPath)
        {
            try
            {
                string libreOfficePath = //@"/usr/lib/libreoffice/program/soffice";
                @"C:\Users\kirill\Desktop\LibreOfficePortable\App\libreoffice\program\soffice.exe";
                ProcessStartInfo procStartInfo = new ProcessStartInfo(libreOfficePath, string.Format("--convert-to pdf --nologo {0}", Path));
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                procStartInfo.WorkingDirectory = EnvironmentDirectoryPath;
                Process process = new Process() { StartInfo = procStartInfo };
                process.Start();
                process.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
        }

        public static void ChangeBookmark(Body body, string bkName, string replaceText)
        {
            try
            {
                var res = from bm in body.Descendants<BookmarkStart>()
                          where bm.Name == bkName
                          select bm;
                Run bookmarkText = res.FirstOrDefault().NextSibling<Run>();
                if (bookmarkText != null)
                {
                    bookmarkText.GetFirstChild<Text>().Text = replaceText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
        }
    } 
}
