﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NBeeNET.Mjolnir.Storage.NetCoreTests.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            return RedirectToPage("UploadImage");
        }
    }
}
