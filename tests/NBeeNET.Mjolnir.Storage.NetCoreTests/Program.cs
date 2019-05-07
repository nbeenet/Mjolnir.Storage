﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NBeeNET.Mjolnir.Storage.Image.Jobs;
using NBeeNET.Mjolnir.Storage.Office.Jobs;

namespace NBeeNET.Mjolnir.Storage.NetCoreTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string path = @"C:\Users\94885\Desktop\123.docx";
            //string path = @"C:\Users\94885\Desktop\123.pdf";
            string path = @"C:\Users\94885\Desktop\123.xlsx";
            new PrintJob().Run(path,null);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
