using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Application.Services.AuthService
{
    public interface IAuthService
    {
      
        Task<string> GenerateAuthorizationTokenAsync(AppUser user);
        Task<string> GenerateCode(string userId);

    }
}
