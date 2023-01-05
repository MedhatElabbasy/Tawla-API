using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.CodeEncrypt
{
    public interface IEncryptDecryptService
    {
        public string Decrypt(string input);
        public string Encrypt(string input);
    }
}
