using ContractsApi.Models;
using ContractsApi.Services;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using io = System.IO;

namespace ContractsApi.Core
{
    public static class TemplateBuilder
    {
        public static string GetAppRoot()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            return directory;
        }
        public static void DMSDocToPdfMain(string Path, string EnvironmentDirectoryPath)
        {
            try
            {
                string libreOfficePath = @"C:\Program Files\LibreOffice\program\soffice.exe";
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
        public static ContractModel GenerateDocument(ContractModel model)
        {
            Console.WriteLine(GetAppRoot());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            io.File.Copy(io.Path.Join(GetAppRoot(),@"\Templates\template.docx"), io.Path.Join(GetAppRoot(), @"\Templates\templateCopy.docx"), true);
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(io.Path.Join(GetAppRoot(), @"\Templates\templateCopy.docx"), true))
                {
                    HtmlConverter converter = new HtmlConverter(wordDoc.MainDocumentPart);
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    TemplateBuilder.ChangeBookmark(body, "ФамилияСтудента", model.Student.LastName);
                    TemplateBuilder.ChangeBookmark(body, "ИмяСтудента", model.Student.FirstName);
                    TemplateBuilder.ChangeBookmark(body, "ОтчествоСтудента", model.Student.Patronymic);
                    TemplateBuilder.ChangeBookmark(body, "ДатаДоговора", model.Date.ToString());
                    TemplateBuilder.ChangeBookmark(body, "ФамилияЗаказчика", model.Payer.LastName);
                    TemplateBuilder.ChangeBookmark(body, "ИмяЗаказчика", model.Payer.FirstName);
                    TemplateBuilder.ChangeBookmark(body, "ОтчествоЗаказчика", model.Payer.Patronymic);
                    TemplateBuilder.ChangeBookmark(body, "ДатаДоговора2", model.Date.ToString());
                    TemplateBuilder.ChangeBookmark(body, "АдресФилиала", model.University.Requisites.ChildAddress);
                    TemplateBuilder.ChangeBookmark(body, "ТелефонФилиала", model.University.Requisites.ChildPhone);
                    TemplateBuilder.ChangeBookmark(body, "ФаксФилиала", model.University.Requisites.ChildFax);
                    TemplateBuilder.ChangeBookmark(body, "EmailФилиала", model.University.Requisites.ChildEmail);
                    TemplateBuilder.ChangeBookmark(body, "ИНН", model.University.Requisites.ChildINN);
                    TemplateBuilder.ChangeBookmark(body, "КПП", model.University.Requisites.ChildKPP);
                    TemplateBuilder.ChangeBookmark(body, "УФК", model.University.Requisites.ChildUFK);
                    TemplateBuilder.ChangeBookmark(body, "БИК", model.University.Requisites.ChildBIK);
                    TemplateBuilder.ChangeBookmark(body, "КазначейскийСчет", model.University.Requisites.ChildKS);
                    TemplateBuilder.ChangeBookmark(body, "ЕдиныйКазначейскийСчет", model.University.Requisites.ChildEKS);
                    TemplateBuilder.ChangeBookmark(body, "ОКВЭД", model.University.Requisites.ChildOKVED);
                    TemplateBuilder.ChangeBookmark(body, "ОКПО", model.University.Requisites.ChildOKPO);
                    TemplateBuilder.ChangeBookmark(body, "НазначениеПлатежа", model.University.Requisites.ChildPurposePayment);
                }
                DMSDocToPdfMain(io.Path.Join(GetAppRoot(), @"\Templates\templateCopy.docx"), io.Path.Join(GetAppRoot(), @"\Output"));
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
