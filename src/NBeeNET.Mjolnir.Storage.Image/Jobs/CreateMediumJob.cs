﻿using NBeeNET.Mjolnir.Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NBeeNET.Mjolnir.Storage.Image.Jobs
{
    /// <summary>
    /// 生成预览图
    /// </summary>
    public class CreateMediumJob
    {
        public JsonFileValues Run(string tempFilePath,JsonFileValues job)
        {
            FileInfo fileInfo = new FileInfo(tempFilePath);
            var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");

            string thumbnailName = string.Format("{0}_{1}{2}", fileName, "Medium", fileInfo.Extension);
            string thumbnailPath = fileInfo.DirectoryName + "\\" + thumbnailName;

            ThumbnailClass.MakeThumbnail(tempFilePath, thumbnailPath, 200, 200, "Cut");
            job.Param = "200x200";
            job.Status = "1";
            job.Value = Core.StorageOperation.GetUrl(thumbnailName);
            job.CreateTime = DateTime.Now;

            return job;
        }
    }
}
