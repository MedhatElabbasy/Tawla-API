using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Application.Services.TimeCode
{
    public interface ITimeCodeService
    {
        public string GenerateTimeCode();
        public Task<string> AddTimeCode(TempCode timeCode);
        public Task<int> Validation(string code, string aspNetUser);
    }
}
