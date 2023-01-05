using Tawala.Infrastructure.Common.Models;
using System.Threading.Tasks;
using Tawala.Domain.Enums;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Infrastructure.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<string> CreateUserAsync(string phone, string role, int countryId);
        string GetRoleByType(AuthTypes type);
        Task<AppIdentityRole> GetRoleTranslated(AppUser user);
        Task<Result> DeleteUserAsync(string userId);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AuthTypes> GetUserTypeByRole(AppUser user);
    }
}
