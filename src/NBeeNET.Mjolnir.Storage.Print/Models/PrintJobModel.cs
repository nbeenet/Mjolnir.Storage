﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NBeeNET.Mjolnir.Storage.Print.Models
{
    /// <summary>
    /// 打印机任务对象
    /// </summary>
    public class PrintJobModel
    {

        public void CopyFrom(PrintJobModel printJobModel)
        {
            this.JobId = printJobModel.JobId;
            this.Name = printJobModel.Name;
            this.DriverName = printJobModel.DriverName;
            this.Document = printJobModel.Document;
            this.JobStatus = printJobModel.JobStatus;
            this.TimeSubmitted = printJobModel.TimeSubmitted;
            this.DataType = printJobModel.DataType;
            this.PagesPrinted = printJobModel.PagesPrinted;
            this.Size = printJobModel.Size;
            this.TotalPages = printJobModel.TotalPages;
        }
        /// <summary>
        /// 任务Id
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Job状态
        /// </summary>
        public string JobStatus { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime TimeSubmitted { get; set; }

        /// <summary>
        /// 文档数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 已打印页数
        /// </summary>
        public int PagesPrinted { get; set; }

        /// <summary>
        /// 已打印文档大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }


    }
}
