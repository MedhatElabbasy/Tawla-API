using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.Exceptions
{
    public class ProblemDetailsCustom : ProblemDetails
    {
        public string Message { get; set; }
    }
}
