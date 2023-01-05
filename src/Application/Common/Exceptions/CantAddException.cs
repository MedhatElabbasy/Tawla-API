using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.Exceptions
{

    public class CantAddException : Exception
    {
        public string _Message { get; set; }
        public CantAddException(string exMessage)
        {
            _Message = exMessage;
        }
        public override string Message
        {
            get { return _Message; }
        }
    }
}
