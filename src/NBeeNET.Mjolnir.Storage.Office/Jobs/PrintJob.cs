﻿using NBeeNET.Mjolnir.Storage.Core.Interface;
using NBeeNET.Mjolnir.Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace NBeeNET.Mjolnir.Storage.Office.Jobs
{
    /// <summary>
    /// 打印job
    /// </summary>
    public class PrintJob : IJob
    {
        public JsonFileValues Run(string tempFilePath)
        {
            Console.WriteLine("开始打印");
            Console.WriteLine("打印文件路径:" + tempFilePath);
            JsonFileValues job = new JsonFileValues();
            FileInfo fileInfo = new FileInfo(tempFilePath);
            try
            {
                //var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                switch (fileInfo.Extension)
                {
                    case ".xls":
                    case ".xlsx":
                        PrintExcel(tempFilePath);
                        break;
                    case ".doc":
                    case ".docx":
                        PrintDoc(tempFilePath);
                        break;
                    case ".pdf":
                        PrintPDF(tempFilePath);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            job.Key = "Print";
            job.Param = "";
            job.Status = "1";
            job.Value = Core.StorageOperation.GetUrl(fileInfo.Name);
            job.CreateTime = DateTime.Now;
            Console.WriteLine("结束打印");
            return job;
        }
        /// <summary>
        /// 打印excel
        /// </summary>
        /// <param name="filePath"></param>
        public bool PrintExcel(string filePath)
        {
            if (!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                return false;
            // start excel and turn off msg boxes
            NetOffice.ExcelApi.Application excelApp = new NetOffice.ExcelApi.Application();
            excelApp.DisplayAlerts = false;

            // create a utils instance, not need for but helpful to keep the lines of code low
            NetOffice.ExcelApi.Tools.CommonUtils utils = new NetOffice.ExcelApi.Tools.CommonUtils(excelApp);
            try
            {
                NetOffice.ExcelApi.Workbook workBook = excelApp.Workbooks.Add();
                //NetOffice.ExcelApi.Workbook workBook = excelApp.Workbooks.Open(filePath);
                var data = workBook.Worksheets[1];
                NetOffice.ExcelApi.Worksheet workSheet = (NetOffice.ExcelApi.Worksheet)workBook.Worksheets[1];

                workSheet.PrintOut();
                string PDFPath = filePath.Replace(".xlsx", ".pdf");
                workBook.ExportAsFixedFormat(NetOffice.ExcelApi.Enums.XlFixedFormatType.xlTypePDF, PDFPath);
                excelApp.Workbooks.Close();
                excelApp.Quit();
                excelApp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                excelApp.Workbooks.Close();
                excelApp.Quit();
                excelApp.Dispose();
                return false;
            }
        }
        /// <summary>
        /// 打印word
        /// </summary>
        /// <param name="filePath"></param>
        public bool PrintDoc(string filePath)
        {
            // start word and turn off msg boxes
            NetOffice.WordApi.Application wordApplication = new NetOffice.WordApi.Application();
            wordApplication.DisplayAlerts = NetOffice.WordApi.Enums.WdAlertLevel.wdAlertsNone;

            // create a utils instance, not need for but helpful to keep the lines of code low
            NetOffice.WordApi.Tools.CommonUtils utils = new NetOffice.WordApi.Tools.CommonUtils(wordApplication);

            // add a new document
            NetOffice.WordApi.Document newDocument = wordApplication.Documents.Open(filePath);

            //newDocument.PrintOut();
            string PDFPath = filePath.Replace(".docx", ".pdf");
            newDocument.ExportAsFixedFormat(PDFPath, NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
            // close word and dispose reference
            newDocument.Close();
            wordApplication.Quit();
            wordApplication.Dispose();
            return true;
            // show dialog for the user(you!)
            //HostApplication.ShowFinishDialog(null, documentFile);
        }
        /// <summary>
        /// 打印pdf
        /// </summary>
        /// <param name="filePath"></param>
        public void PrintPDF(string filePath)
        {
            //PrintDocument    
            //Create a pdf document.
            //PdfDocument doc = new PdfDocument();
            //doc.LoadFromFile(filePath);
            //doc.Print();
            //doc.Close();
            using (var proc = CreateProcess(filePath))
            {
                proc.Start();
                bool result = proc.WaitForExit(30000);
                //if (!result)
                //{
                //    proc.Kill();
                //}
            }
        }

        private static readonly string utilPath = GetUtilPath();
        
        private static Process CreateProcess(string filePath)
        {
            return new Process
            {
                StartInfo =
                    {
                    WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = utilPath,
                        Arguments = $@"""{filePath}""",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
            };
        }

        private static string GetUtilPath()
        {
            return Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "PDFtoPrinter.exe");
        }
    }
}

