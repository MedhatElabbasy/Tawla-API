using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Domain.Entities.Identity;
using Tawala.Infrastructure.Common.Interfaces;
using Tawala.Infrastructure;

namespace Tawala.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService identityService;
        private readonly IIdentityRepository identityRepo;
        private readonly IMapper mapper;
        private readonly JwtHandler jwtHandler;
        //private readonly IRepository<Teacher> teacherRepository;
        //private readonly IRepository<Student> studentRepository;
        private readonly UserManager<AppUser> userManager;
        public AuthService(IIdentityService identityService,
            IIdentityRepository repository,
            IMapper mapper,
            JwtHandler jwtHandler,
            //IRepository<Teacher> _teacherRepository,
            //IRepository<Student> _studentRepository,
            UserManager<AppUser> _userManager
            )
        {
            this.identityService = identityService;
            this.identityRepo = repository;
            this.mapper = mapper;
            userManager = _userManager;
            this.jwtHandler = jwtHandler;
            //this.teacherRepository = _teacherRepository;
            //this.studentRepository = _studentRepository;
            this.userManager = _userManager;
        }
        //public async Task<string> RegisterUserAsync(RegisterDto model)
        //{
        //    //register the user by phone
        //    var result = await identityService.CreateUserAsync(model.phone, model.role, model.phoneCountryId.Value);

        //    return result;
        //}
        //public async Task<AppUserAuthDTO> ActivateAccountAsync(string userId)
        //{
        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        var user = await identityRepo.Get(userId);

        //        if (user != null)
        //        {
        //            var roles = await userManager.GetRolesAsync(user);
        //            var userDto = mapper.Map<AppUserDTO>(user);
        //            userDto.Roles = roles;
        //            //TeacherDTO teacher = null;
        //            //StudentDTO student = null;

        //            if (roles.Contains("Teacher"))
        //                //   teacher = await GetTeacher(userId);
        //                if (roles.Contains("Student"))
        //                    // student = await GetStudent(userId);

        //                    if (user.IsActive)
        //                        return new AppUserAuthDTO()
        //                        {
        //                            AppUser = user,
        //                            AppUserDTO = userDto,
        //                            // Teacher = teacher,
        //                            // Student = student
        //                        };
        //            //Update User State
        //            user.IsActive = true;
        //            var userUpdated = await identityRepo.Update(user);
        //            var userUpdatedDto = mapper.Map<AppUserDTO>(user);
        //            userUpdatedDto.Roles = roles;
        //            return new AppUserAuthDTO()
        //            {
        //                AppUser = userUpdated,
        //                AppUserDTO = userUpdatedDto,
        //                // Teacher = teacher,
        //                // Student = student
        //            }; ;
        //        }
        //    }
        //    return null;
        //}
        public async Task<string> GenerateAuthorizationTokenAsync(AppUser user)
        {
            try
            {
                var signingCredentials = jwtHandler.GetSigningCredentials();
                var claims = await jwtHandler.GetClaimsAsync(user);
                var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return token;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        public async Task<string> GenerateCode(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var code = await userManager.GenerateTwoFactorTokenAsync(user, "PhoneCode");
            return code;
        }


    }
}
