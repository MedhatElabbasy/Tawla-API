using Tawala.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.Utility
{
    public static class RequestUtility
    {
        public static UserLanguage? Language { get; set; }
        public static bool IsArabic { get; set; }
        public static Source? Source { get; set; }
       
    }
}
