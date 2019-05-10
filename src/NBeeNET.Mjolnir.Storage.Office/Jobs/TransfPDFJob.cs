﻿using NBeeNET.Mjolnir.Storage.Core.Interface;
using NBeeNET.Mjolnir.Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NBeeNET.Mjolnir.Storage.Office.Jobs
{
    /// <summary>
    /// office文件转pdf
    /// </summary>
    public class TransfPDFJob : IJob
    {
        public JsonFileValues Run(string tempFilePath)
        {
            JsonFileValues job = new JsonFileValues();
            string targetPath = tempFilePath;
            FileInfo fileInfo = new FileInfo(tempFilePath);
            job.Key = "Print";
            job.Param = "";
            job.CreateTime = DateTime.Now;
            try
            {
                //var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                switch (fileInfo.Extension)
                {
                    case ".xls":
                    case ".xlsx":
                        ExcelToPDF(tempFilePath, targetPath);
                        break;
                    case ".doc":
                    case ".docx":
                        WordToPDF(tempFilePath, targetPath);
                        break;
                }
                job.Status = "1";
                job.Value = Core.StorageOperation.GetUrl(fileInfo.Name);
            }
            catch (Exception ex)
            {
                job.Status = "0";
                job.Value = ex.ToString();
                Console.WriteLine(ex.ToString());
            }
            return job;
        }
        /// <summary>
        /// excel转pdf
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void ExcelToPDF(string sourcePath, string targetPath)
        {
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            workbook.LoadFromFile(sourcePath);
            workbook.SaveToFile(targetPath, Spire.Xls.FileFormat.PDF);
            workbook.Dispose();
        }
        /// <summary>
        /// word转pdf
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void WordToPDF(string sourcePath, string targetPath)
        {
            Spire.Doc.Document document = new Spire.Doc.Document();
            document.LoadFromFile(sourcePath);
            document.SaveToFile(targetPath, Spire.Doc.FileFormat.PDF);
            document.Close();
        }

    }
}