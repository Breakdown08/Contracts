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
        public static void DMSDocToPdfMain(string Path, string EnvironmentDirectoryPath)
        {
            try
            {
                string libreOfficePath = //@"/usr/lib/libreoffice/program/soffice";
                @"C:\Program Files\LibreOffice\program\soffice.exe";
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
            io.File.Copy(@"C:\Users\Kirill\Desktop\template.docx", @"C:\Users\Kirill\Desktop\templateCopy.docx", true);
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(@"C:\Users\Kirill\Desktop\templateCopy.docx", true))
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
                }

                //Close the handle explicitly.
                //model = @"C:\Users\Kirill\Desktop\myPDFDocument";
                DMSDocToPdfMain(@"C:\Users\Kirill\Desktop\templateCopy.docx", @"C:\Users\Kirill\Desktop");
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
