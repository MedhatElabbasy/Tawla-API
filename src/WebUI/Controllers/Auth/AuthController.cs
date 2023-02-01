﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Common.Exceptions;
using Tawala.Application.Common.SendEmail;
using Tawala.Application.Common.Utility;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings;
using Tawala.Infrastructure.Common.Interfaces;
using Tawala.Infrastructure;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.MyAppUserDTO;
using Tawala.Application.Services.AuthService;
using MailKit;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.Extensions.Configuration;
using IMailService = Tawala.Application.Common.SendEmail.IMailService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;

namespace Tawala.WebUI.Controllers.Auth
{
    public class AuthController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService identityService;
        private readonly IAuthService authService;
        private readonly IMailService mailService;
        private readonly IConfiguration configuration;
        public AuthController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<AppUser> userManager
,
            IIdentityService identityService,
            IAuthService authService,
            Application.Common.SendEmail.IMailService mailService,
            IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            _userManager = userManager;
            this.identityService = identityService;
            this.authService = authService;
            this.mailService = mailService;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<AppUserDTO> RegisterAdmin(AppUserDTO model)
        {
            //check if user is found
            var user = await identityService.GetUserByUserNameAsync(model.UserName);
            if (user == null)
            {
                //create new User
                var newUser = mapper.Map<AppUser>(model);
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {

                    var createdUser = await identityService.GetUserByUserNameAsync(model.UserName);

                    return mapper.Map<AppUserDTO>(createdUser);
                }
                else
                {
                    throw new GlobalException(exMessage: RequestUtility.IsArabic ? "حدث خطأ" : "An error occurred");
                }
            }
            else
            {
                throw new GlobalException(exMessage: RequestUtility.IsArabic ? "هذا المستخدم موجود من قبل" : "This user already exists");
            }
        }

        [HttpPost]
        [Route("LoginAdmin")]
        public async Task<ActionResult> LoginAdmin(string userName, string password)
        {
            var user = await identityService.GetUserByUserNameAsync(userName);

            if (user != null)
            {
                var validCredentials = await _userManager.CheckPasswordAsync(user, password);
                if (validCredentials)
                {

                    var token = await authService.GenerateAuthorizationTokenAsync(user);
                    return Ok(new
                    {
                        user = mapper.Map<AppUserResDTO>(user),
                        token = token
                    });
                }
                else
                {
                    throw new GlobalException(exMessage: RequestUtility.IsArabic ? " اسم المستخدم او كلمة المرور غير صحيحة" : "The username or password is incorrect");
                }
            }
            else
            {
                throw new GlobalException(exMessage: RequestUtility.IsArabic ? " اسم المستخدم او كلمة المرور غير صحيحة" : "The username or password is incorrect");
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<bool> ChangePassword(string AppuserId, string userName, string Newpassword, string OldPassword)
        {
            var user = await _userManager.FindByIdAsync(AppuserId);
            if (user == null)
                throw new GlobalException(exMessage: RequestUtility.IsArabic ? "مستخدم غير موجود" : "User not found");

            var checkOldPassword =
                 await _userManager.ChangePasswordAsync(user, OldPassword, Newpassword);

            if (!checkOldPassword.Succeeded)
                throw new GlobalException(exMessage: RequestUtility.IsArabic ? "غير مطابق" : "Not matching");

            return true;
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(string userName)
        {
            var user = await identityService.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                throw new GlobalException(exMessage: "مستخدم غير موجود");
            }
            else
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = configuration.GetSection("PathSetting:ResetPassword").Value;
                resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
                try
                {

                    await mailService.SendEmailAsync(new MailRequest()
                    {
                        Body = "اضغط علي الرابط التالي للاستعادة كلمة المرور  " + Environment.NewLine + "<a href='" + result + resetToken + "&email=" + user.UserName + "' target=\"_blank\">اضغط</a> ",
                        ToEmail = user.Email,
                        Subject = "استعادة كلمة المرور",
                    });
                }
                catch (Exception)
                {

                }
                //  return code2;
                return Ok(new { message = "تم ارسال الرابط علي الميل بنجاح" });
            }
        }

        [HttpPost]
        [Route("ForgetPasswordConfirm")]
        public async Task<bool> ForgetPasswordConfirm(string userName, string resetToken, string Newpassword)
        {


            var bytes = WebEncoders.Base64UrlDecode(resetToken);
            var code = Encoding.UTF8.GetString(bytes);

            var user = await identityService.GetUserByUserNameAsync(userName);
            var result = await _userManager.ResetPasswordAsync(user, code, Newpassword);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("updateUser")]
        public async Task<AppUserResDTO> UpdateUser(AppUserResDTO model)
        {
            var user = await identityService.GetUserByUserNameAsync(model.UserName);
            user.PhoneNumber = model.PhoneNumer;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;
            user.IsAdmin = model.IsAdmin;
            user.IsUser = model.IsUser;
            user.IsRestAdmin = model.IsRestAdmin;
            user.IsRestUser = model.IsRestUser;
            var res = context.Users.Update(user);
            await context.SaveChangesAsync();
            //if (res.Succeeded)
            //{
            return model;
            // }
            //else
            //{
            //    throw new GlobalException(exMessage: RequestUtility.IsArabic ? "حدث خظأ" : "Error Ocared");
            //}
        }


        [HttpGet]
        [Route("GetAllAdmin")]
        public async Task<List<AppUserResDTO>> GetAllAdmin()
        {

            var res = await context.Users.Where(x => x.IsAdmin).
                                    Include(x => x.Photo).
                                    ToListAsync();

            return mapper.Map<List<AppUserResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllAdminUser")]
        public async Task<List<AppUserResDTO>> GetAllAdminUser()
        {

            var res = await context.Users.Where(x => x.IsAdminUser).
                                    Include(x => x.Photo).
                                    ToListAsync();

            return mapper.Map<List<AppUserResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllRestUser")]
        public async Task<List<AppUserResDTO>> GetAllRestUser()
        {

            var res = await context.Users.Where(x => x.IsRestUser).
                                    Include(x => x.Photo).
                                    ToListAsync();

            return mapper.Map<List<AppUserResDTO>>(res);
        }


        [HttpGet]
        [Route("GetAllRestAdmin")]
        public async Task<List<AppUserResDTO>> GetAllRestAdmin()
        {

            var res = await context.Users.Where(x => x.IsRestAdmin).
                                    Include(x => x.Photo).
                                    ToListAsync();

            return mapper.Map<List<AppUserResDTO>>(res);
        }



    }
}
