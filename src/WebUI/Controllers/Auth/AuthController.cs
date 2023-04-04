using AutoMapper;
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
using Tawala.Application.Services.TimeCode;
using Tawala.Application.Common.MessageService;
using Tawala.Infrastructure.Common.Models;
using static IdentityServer4.Models.IdentityResources;
using Tawala.Application.Models.ServiceProviderDTO;

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
        private readonly ITimeCodeService timeCodeService;

        private readonly ISMSService _sMSService;
        public AuthController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<AppUser> userManager
,
            IIdentityService identityService,
            IAuthService authService,
            ITimeCodeService timeCodeService,
            Application.Common.SendEmail.IMailService mailService,
            IConfiguration configuration,
            ISMSService sMSService)
        {
            this.context = context;
            this.mapper = mapper;
            _userManager = userManager;
            this.identityService = identityService;
            this.authService = authService;
            this.timeCodeService = timeCodeService;
            this.mailService = mailService;
            this.configuration = configuration;
            _sMSService = sMSService;
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<AppUserResDTO> RegisterAdmin(AppUserDTO model)
        {
            //check if user is found
            var user = await identityService.GetUserByUserNameAsync(model.Email);
            if (user == null)
            {
                //create new User

                var newUser = mapper.Map<AppUser>(model);
                newUser.UserName = model.Email;
                if (model.IsUser)
                {
                    newUser.IsActive = false;
                }
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {

                    var createdUser = await identityService.GetUserByUserNameAsync(model.Email);
                    if (model.IsUser)
                    {
                        await GenerateTotpCode(model.PhoneNumer, model.Email, createdUser.Id);
                        var newRes = mapper.Map<AppUserResDTO>(createdUser);
                        newRes.IsActive = false;


                        return mapper.Map<AppUserResDTO>(createdUser);

                    }

                    return mapper.Map<AppUserResDTO>(createdUser);
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



        //[HttpPost]
        //[Route("RegisterUser")]
        //public async Task<AppUserDTO> RegisterUser(AppUserDTO model)
        //{
        //    //check if user is found
        //    var user = await identityService.GetUserByUserNameAsync(model.PhoneNumer);
        //    if (user == null)
        //    {
        //        //create new User

        //        var newUser = mapper.Map<AppUser>(model);
        //        newUser.UserName = model.PhoneNumer;
        //        var result = await _userManager.CreateAsync(newUser, "Takid#2022");
        //        if (result.Succeeded)
        //        {

        //            var createdUser = await identityService.GetUserByUserNameAsync(model.PhoneNumer);

        //            return mapper.Map<AppUserDTO>(createdUser);
        //        }
        //        else
        //        {
        //            throw new GlobalException(exMessage: RequestUtility.IsArabic ? "حدث خطأ" : "An error occurred");
        //        }
        //    }
        //    else
        //    {
        //        throw new GlobalException(exMessage: RequestUtility.IsArabic ? "هذا المستخدم موجود من قبل" : "This user already exists");
        //    }
        //}

        [HttpPost]
        [Route("LoginAdmin")]
        public async Task<ActionResult> LoginAdmin(string userName, string password)
        {
            var user = await identityService.GetUserByUserNameAsync(userName);


            if (user.IsActive == false && user.IsUser)
            {

                await GenerateTotpCode(user.PhoneNumer, user.Email, user.Id);
                return Ok(new
                {
                    message = "تم ارسال كود التحقق علي الميل ورقم الجوال الخاص بك"

                });

            }
            if (user != null)
            {
                var validCredentials = await _userManager.CheckPasswordAsync(user, password);
                if (validCredentials)
                {

                    var token = await authService.GenerateAuthorizationTokenAsync(user);
                    return Ok(new
                    {
                        user = mapper.Map<AppUserResDTO>(user),
                        token = token,
                        restaurant = await context.Restaurants.Where(x => x.Id == user.UserRestaurantId).FirstOrDefaultAsync(),
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
        [Route("LoginUser")]
        public async Task<ActionResult> LoginUser(string userName)
        {
            var user = await identityService.GetUserByUserNameAsync(userName);

            if (user != null)
            {
                //var validCredentials = await _userManager.FindByEmailAsync(user );
                //if (validCredentials)
                //{

                var token = await authService.GenerateAuthorizationTokenAsync(user);
                return Ok(new
                {
                    user = mapper.Map<AppUserResDTO>(user),
                    token = token,
                    restaurant = await context.Restaurants.Where(x => x.Id == user.UserRestaurantId).FirstOrDefaultAsync(),
                });
                //}
                //else
                //{
                //    throw new GlobalException(exMessage: RequestUtility.IsArabic ? " اسم المستخدم او كلمة المرور غير صحيحة" : "The username or password is incorrect");
                //}
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
            user.PhotoId = model.PhotoId;
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

        [HttpPost]
        [Route("UpdateUserRest")]
        public async Task<AppUserResDTO> UpdateUserRest(string userId, Guid restId)
        {
            var user = await identityService.GetUserByIdAsync(userId);
            user.UserRestaurantId = restId;

            var res = context.Users.Update(user);
            await context.SaveChangesAsync();
            //if (res.Succeeded)
            //{
            return mapper.Map<AppUserResDTO>(user);
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


        [HttpGet]
        [Route("GetById")]
        public async Task<AppUserResDTO> GetById(Guid Id)
        {

            var res = await context.Users.Where(x => x.Id == Id.ToString()).
                                    Include(x => x.Photo).

                                    FirstOrDefaultAsync();

            var photo = await context.AppAttachment.Where(x => x.Id == res.PhotoId).FirstOrDefaultAsync();
            res.Photo = photo;
            var newresult = mapper.Map<AppUserResDTO>(res);
            newresult.Restaurant = mapper.Map<RestaurantResDTO>(await context.Restaurants.Where(x => x.Id == res.UserRestaurantId).FirstOrDefaultAsync());
            return newresult;
        }

        private async Task<string[]> GenerateTotpCode(string phoneNumber, string email, string aspnetuserId)
        {

            //new code 
            var newCode = await timeCodeService.AddTimeCode(new TempCode() { AppUserId = aspnetuserId });


            try
            {
                if (phoneNumber != "")
                {
                    await _sMSService.SendMessageAsync(new SmsModel()
                    {
                        Msg = "Your Tawala verification code is " + newCode,
                        Numbers = phoneNumber
                    });
                }
                else if (email != "")
                {
                    //send email
                    await mailService.SendEmailAsync(new MailRequest()
                    {
                        Body = " كود التحقق الخاص بك " + newCode,
                        ToEmail = email,
                        Subject = "كود التحقق",
                    });

                }


            }
            catch (Exception ex)
            {

                throw new GlobalException(exMessage: "حدث خطأ في إرسال الرسالة");
            }
            return new string[] { newCode, "" };

        }


        //private async Task<int> VerifyTotpCode(string code, string appUserId)
        //{
        //    var res = await timeCodeService.Validation(code, aspNetUser: appUserId);
        //    return res;
        //}


        //private async Task<int> VerifyTotpCode2(string code, string appUserId)
        //{
        //    var res = await timeCodeService.Validation(code, aspNetUser: appUserId);
        //    return res;
        //}




        [HttpPost]
        [Route("VerifyTotpCode")]
        public async Task<IActionResult> VerifyTotpCode(string code, string appUserId)
        {


            var user = await identityService.GetUserByIdAsync(appUserId);

            var res2 = await timeCodeService.Validation(code, user.Id);
            switch (res2)
            {
                case -2:
                    return BadRequest(new
                    {
                        Success = 1,
                        Message = RequestUtility.IsArabic ? "كود التحقق غير صحيح" : "Invalid verification code",
                        status = "error"
                    });
                case -1:
                    return BadRequest(new
                    {
                        Success = 1,
                        Message = RequestUtility.IsArabic ? "كود التحقق انتهت صلاحيته" : "Verification code has expired",
                        status = "error"
                    });
                case 0:
                    return BadRequest(new
                    {
                        Success = 1,
                        Message = RequestUtility.IsArabic ? "كود التحقق انتهت صلاحيته" : "Verification code has expired",
                        status = "error"
                    });
                case 1:

                    user.IsActive = true;

                    var res = context.Users.Update(user);
                    await context.SaveChangesAsync();

                    var token = await authService.GenerateAuthorizationTokenAsync(user);
                    return Ok(new
                    {
                        user = mapper.Map<AppUserResDTO>(user),
                        token = token,
                        restaurant = await context.Restaurants.Where(x => x.Id == user.UserRestaurantId).FirstOrDefaultAsync(),
                    });

                default:
                    break;
            }
            return Ok(new
            {
                message = ""
            });
        }
    }
}
