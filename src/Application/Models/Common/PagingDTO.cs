using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Models.Common
{
    public class PagingDTO<T> where T : class
    {

        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int CountInPage { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
