using Tawala.Infrastructure.Common.Interfaces;
using Tawala.Infrastructure.Common.Models;
using Tawala.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tawala.Domain.Enums;
using Tawala.Domain.Entities.Identity;
using System.Collections.Generic;
namespace Tawala.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly MyConfigService myConfig;

        public IdentityService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            MyConfigService myConfig
            )
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            this.myConfig = myConfig;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                    return user.UserName;
                else
                    return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                return user;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> CreateUserAsync(string phone, string role, int countryId)
        {
            var user = new AppUser
            {
                UserName = phone,
                IsActive = false,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, myConfig.options.DefaultIdentityPassword);
                if (result.Succeeded)
                    result = await _userManager.AddToRoleAsync(user, role);
                if (result.Succeeded)
                    return user.Id;
                else
                    return null;

            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public string GetRoleByType(AuthTypes type)
        {
            var res = "";
            switch (type)
            {
                case AuthTypes.Company:
                    res = "Company";
                    break;
                case AuthTypes.SecuritySupervisor:
                    res = "SecuritySupervisor";
                    break;
                case AuthTypes.SecuritCompany:
                    res = "SecuritCompany";
                    break;
                case AuthTypes.SecurityGurd:
                    res = "SecurityGurd";
                    break;
                case AuthTypes.Admin:
                    res = "Admin";
                    break;
                case AuthTypes.GovernmentUser:
                    res = "GovernmentUser";
                    break;
                case AuthTypes.SecurityCompanyUser:
                    res = "SecurityCompanyUser";
                    break;
                case AuthTypes.ClientCompanyUser:
                    res = "ClientCompanyUser";
                    break;
                case AuthTypes.RasdUser:
                    res = "RasdUser";
                    break;
                case AuthTypes.Agent:
                    res = "Agent";
                    break;
                default:
                    break;
            }

            return res;
        }
        public async Task<AuthTypes> GetUserTypeByRole(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var AuthTypesRes = new AuthTypes();
            roles.ToList().ForEach(role =>
            {
                if (role == "SecurityGurd")
                    AuthTypesRes = AuthTypes.SecurityGurd;
                else if (role == "Company")
                    AuthTypesRes = AuthTypes.Company;
                else if (role == "SecuritySupervisor")
                    AuthTypesRes = AuthTypes.SecuritySupervisor;
                else if (role == "SecuritCompany")
                    AuthTypesRes = AuthTypes.SecuritCompany;
                else if (role == "Admin")
                    AuthTypesRes = AuthTypes.Admin;
                else if (role == "GovernmentUser")
                    AuthTypesRes = AuthTypes.GovernmentUser;
                else if (role == "SecurityCompanyUser")
                    AuthTypesRes = AuthTypes.SecurityCompanyUser;
                else if (role == "ClientCompanyUser")
                    AuthTypesRes = AuthTypes.ClientCompanyUser;
                else if (role == "RasdUser")
                    AuthTypesRes = AuthTypes.RasdUser;
                else if (role == "Agent")
                    AuthTypesRes = AuthTypes.Agent;
            });

            return AuthTypesRes;
        }

        public async Task<AppIdentityRole> GetRoleTranslated(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = new List<AppIdentityRole>(){  new AppIdentityRole
                {
                    Name = "Admin",
                    NameEn = "Admin",
                    NameAR = "مدير",
                    NormalizedName = "ADMIN"
                },
                new AppIdentityRole
                {
                    Name = "Company",
                    NameEn = "Company",
                    NameAR = "شركة",
                    NormalizedName = "Company".ToUpper()
                },
                 new AppIdentityRole
                 {
                     Name = "SecurityGurd",
                     NameEn = "Security Guard",
                     NameAR = "حارس أمن",
                     NormalizedName = "SecurityGurd".ToUpper()
                 },
               new AppIdentityRole
               {
                   Name = "SecuritySupervisor",
                   NameEn = "Security Supervisor",
                   NameAR = "مشرف أمن",
                   NormalizedName = "SecuritySupervisor".ToUpper()
               },
                new AppIdentityRole
                {
                    Name = "SecuritCompany",
                    NameEn = "Security Company",
                    NameAR = "شركة أمن",
                    NormalizedName = "SecuritCompany".ToUpper()
                }, new AppIdentityRole
                {
                    Name = "SecurityCompanyUser",
                    NameEn = "Security Company User",
                    NameAR = "مستخدم نظام",
                    NormalizedName = "SecurityCompanyUser".ToUpper()
                },
                new AppIdentityRole
                {
                    Name = "ClientCompanyUser",
                    NameEn = "Client Company User",
                    NameAR = "مستخدم نظام",
                    NormalizedName = "ClientCompanyUser".ToUpper()
                    }, new AppIdentityRole
                {
                    Name = "RasdUser",
                    NameEn = "Rasd User",
                    NameAR = "عميل رصد",
                    NormalizedName = "RasdUser".ToUpper()
                    },
                new AppIdentityRole
                {
                    NameEn = "Government User",
                    Name = "GovernmentUser",
                    NameAR = "الموظف الحكومي",
                    NormalizedName = "GovernmentUser".ToUpper()

                },
                 new AppIdentityRole
                {
                    NameEn = "Agent",
                    Name = "Agent",
                    NameAR = "وكيل",
                    NormalizedName = "Agent".ToUpper()

                }
            };

            return roles.Where(x => x.Name == userRoles.FirstOrDefault()).FirstOrDefault();
        }
    }
}
