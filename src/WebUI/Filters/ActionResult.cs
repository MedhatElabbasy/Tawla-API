using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tawala.WebUI.Filters
{
    public class Response : ActionResult
    {
        public object Data { get; set; }
        public int code { get; set; }
    }
}
