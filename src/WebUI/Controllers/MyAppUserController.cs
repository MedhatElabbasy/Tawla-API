//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Tawala.Application.Models.MyAppUserDTO;
//using Tawala.Infrastructure.Persistence;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System;
//using Tawala.Domain.Entities.Users;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using Tawala.Application.Common.CodeEncrypt;
//using Tawala.Application.Models.OfferDTO;
//using Tawala.Application.Models;
//using System.Drawing.Printing;
//using Tawala.Application.Common.RealTime.Hubs;

//namespace Tawala.WebUI.Controllers
//{
//    public class MyAppUserController : ApiControllerBase
//    {
//        private readonly ApplicationDbContext context;
//        private readonly IEncryptDecryptService encryptDecryptService;
//        private readonly IMapper mapper;
//        public MyAppUserController(ApplicationDbContext context, IMapper mapper, IEncryptDecryptService encryptDecryptService)
//        {
//            this.context = context;
//            this.mapper = mapper;
//            this.encryptDecryptService = encryptDecryptService;
//        }

//        [HttpPost]
//        [Route("Register")]
//        public async Task<ActionResult> Register(MyAppUserAddDTO model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.PhoneNumber == model.PhoneNumber).
//                                Include(x => x.Photo).
//                                FirstOrDefaultAsync();

//            if (current != null)
//            {
//                return StatusCode(403, new
//                {
//                    Message = "User is Exist",
//                    Status = "Error"
//                });
//            }
//            model.Password = encryptDecryptService.Encrypt(model.Password);
//            var res = mapper.Map<MyAppUserResDTO>(context.MyAppUsers.Add(mapper.Map<MyAppUser>(model)).Entity);
//            context.SaveChanges();


//            var result = await context.MyAppUsers.
//                Where(x => x.Id == res.Id).
//                Include(x => x.Photo).
//                FirstOrDefaultAsync();

//            return Ok(mapper.Map<MyAppUserResDTO>(result));
//        }



//        [HttpPost]
//        [Route("RegisterAdmin")]
//        public async Task<ActionResult> RegisterAdmin(MyAppUserAddDTO model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.Email == model.Email).
//                                Include(x => x.Photo).
//                                FirstOrDefaultAsync();

//            if (current != null)
//            {
//                return StatusCode(403, new
//                {
//                    Message = "User is Exist",
//                    Status = "Error"
//                });
//            }
//            model.Password = encryptDecryptService.Encrypt(model.Password);
//            var res = mapper.Map<MyAppUserResDTO>(context.MyAppUsers.Add(mapper.Map<MyAppUser>(model)).Entity);
//            context.SaveChanges();


//            var result = await context.MyAppUsers.
//                Where(x => x.Id == res.Id).
//                Include(x => x.Photo).
//                FirstOrDefaultAsync();

//            return Ok(mapper.Map<MyAppUserResDTO>(result));
//        }

//        [HttpPost]
//        [Route("Login")]
//        public async Task<ActionResult> Login(MyLoginModel model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.PhoneNumber == model.PhoneNumber).
//                Include(x => x.Photo).
//                FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }
//            //check Password
//            if (current.Password != encryptDecryptService.Encrypt(model.Password))
//            {

//                return NotFound(new
//                {
//                    Message = "User Or Password is Invalid",
//                    Status = "Error"
//                });
//            }
//            //return result
//            return Ok(mapper.Map<MyAppUserResDTO>(current));
//        }

//        [HttpPost]
//        [Route("LoginAdmin")]
//        public async Task<ActionResult> LoginAdmin(MyLoginAdminModel model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.Email == model.Email).
//                Include(x => x.Photo).
//                FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }
//            //check Password
//            if (current.Password != encryptDecryptService.Encrypt(model.Password))
//            {

//                return NotFound(new
//                {
//                    Message = "User Or Password is Invalid",
//                    Status = "Error"
//                });
//            }
//            //return result
//            return Ok(mapper.Map<MyAppUserResDTO>(current));
//        }

//        [HttpPost]
//        [Route("ChangePassword")]
//        public async Task<ActionResult> ChangePassword(ChangePassword model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.PhoneNumber == model.PhoneNumber).
//                                                    Include(x => x.Photo).
//                                                    FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }
//            //check Password
//            if (current.Password != encryptDecryptService.Encrypt(model.OldPassword))
//            {

//                return NotFound(new
//                {
//                    Message = "Password is Invalid",
//                    Status = "Error"
//                });
//            }

//            current.Password = encryptDecryptService.Encrypt(model.NewPassword);

//            await context.SaveChangesAsync();

//            var result = mapper.Map<MyAppUserResDTO>(await context.MyAppUsers.Where(x => x.Id == current.Id).Include(x => x.Photo).FirstOrDefaultAsync());

//            return Ok(result);

//        }


//        [HttpPost]
//        [Route("ChangePasswordAdmin")]
//        public async Task<ActionResult> ChangePasswordAdmin(ChangePasswordAdmin model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.Email == model.Email).
//                                                    Include(x => x.Photo).
//                                                    FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }
//            //check Password
//            if (current.Password != encryptDecryptService.Encrypt(model.OldPassword))
//            {

//                return NotFound(new
//                {
//                    Message = "Password is Invalid",
//                    Status = "Error"
//                });
//            }

//            current.Password = encryptDecryptService.Encrypt(model.NewPassword);

//            await context.SaveChangesAsync();

//            var result = mapper.Map<MyAppUserResDTO>(await context.MyAppUsers.Where(x => x.Id == current.Id).Include(x => x.Photo).FirstOrDefaultAsync());

//            return Ok(result);

//        }

//        [HttpPost]
//        [Route("ForgetPassword")]
//        public async Task<ActionResult> ForgetPassword(ForgetPassword model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.PhoneNumber == model.PhoneNumber).
//                                                    Include(x => x.Photo).
//                                                    FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }

//            current.Password = encryptDecryptService.Encrypt(model.Password);

//            await context.SaveChangesAsync();

//            var result = mapper.Map<MyAppUserResDTO>(await context.MyAppUsers.
//                            Where(x => x.Id == current.Id).
//                            Include(x => x.Photo).
//                            FirstOrDefaultAsync());

//            return Ok(result);

//        }


//        [HttpPost]
//        [Route("ForgetPasswordAdmin")]
//        public async Task<ActionResult> ForgetPasswordAdmin(ForgetPasswordAdmin model)
//        {
//            //check if Phone Number Exist
//            var current = await context.MyAppUsers.Where(x => x.Email == model.Email).
//                                                    Include(x => x.Photo).
//                                                    FirstOrDefaultAsync();
//            if (current == null)
//            {
//                return NotFound(new
//                {
//                    Message = "User is Not Found",
//                    Status = "Error"
//                });
//            }

//            current.Password = encryptDecryptService.Encrypt(model.Password);

//            await context.SaveChangesAsync();

//            var result = mapper.Map<MyAppUserResDTO>(await context.MyAppUsers.
//                            Where(x => x.Id == current.Id).
//                            Include(x => x.Photo).
//                            FirstOrDefaultAsync());

//            return Ok(result);

//        }


//        [HttpPost]
//        [Route("Update")]
//        public async Task<MyAppUserResDTO> Update(MyAppUserUpdateDTO model)
//        {
//            var result = await context.MyAppUsers
//                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

//            result = mapper.Map(model, result);

//            await context.SaveChangesAsync();

//            var fresult = mapper.Map<MyAppUserResDTO>(await context.MyAppUsers.Where(x => x.Id == result.Id).Include(x => x.Photo).FirstOrDefaultAsync());

//            return mapper.Map<MyAppUserResDTO>(fresult);
//        }

//        [HttpPost]
//        [Route("Delete")]
//        public async Task<bool> Delete(Guid Id)
//        {
//            var MyAppUserInDb = await context.MyAppUsers
//                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
//            if (MyAppUserInDb == null) return false;
//            MyAppUserInDb.IsDeleted = true;

//            return await context.SaveChangesAsync() > 0;

//        }

//        [HttpGet]
//        [Route("GetAll")]
//        public async Task<PagingDTO<MyAppUserResDTO>> GetAll(int page, int pageSize)
//        {

//            var res = await context.MyAppUsers.
//                                Where(x => x.IsDeleted == false).
//                                Include(x => x.Photo).
//                                Skip((page - 1) * pageSize).
//                                Take(pageSize).
//                                ToListAsync();


//            return new PagingDTO<MyAppUserResDTO>()
//            {
//                Data = mapper.Map<List<MyAppUserResDTO>>(res),
//                Page = page,
//                PageSize = pageSize,
//                TotalCount = (await context.MyAppUsers.Where(x => x.IsDeleted == false).CountAsync()),
//                CountInPage = res.Count
//            };



//        }

//        [HttpGet]
//        [Route("GetAllAdmin")]
//        public async Task<PagingDTO<MyAppUserResDTO>> GetAllAdmin(int page, int pageSize)
//        {



//            var res = await context.MyAppUsers.
//                                                Where(x => x.IsDeleted == false && x.IsAdmin == true).
//                                                Include(x => x.Photo).
//                                                Skip((page - 1) * pageSize).
//                                                Take(pageSize).
//                                                ToListAsync();


//            return new PagingDTO<MyAppUserResDTO>()
//            {
//                Data = mapper.Map<List<MyAppUserResDTO>>(res),
//                Page = page,
//                PageSize = pageSize,
//                TotalCount = (await context.MyAppUsers.Where(x => x.IsDeleted == false && x.IsAdmin == true).CountAsync()),
//                CountInPage = res.Count
//            };

//        }

//        [HttpGet]
//        [Route("GetById")]
//        public async Task<MyAppUserResDTO> GetById(Guid Id)
//        {

//            var res = await context.MyAppUsers.
//                                Where(x => x.Id == Id).
//                                Include(x => x.Photo).
//                                FirstOrDefaultAsync();

//            return mapper.Map<MyAppUserResDTO>(res);
//        }





//        [HttpGet]
//        [Route("GetUserConnectionId")]
//        public ActionResult GetUserConnectionId(Guid Id)
//        {

//            var res = ChatHub.Users.
//                                Where(x => x.UserId == Id.ToString()).FirstOrDefault();

//            if (res == null)
//            {
//                return Ok(new
//                {
//                    connectionId = ""
//                }) ;
//            }

//            return     Ok(new
//            {
//                connectionId = res.ConnectionId
//            }); ;
//        }

//        [HttpGet]
//        [Route("GetAdminConnectionId")]
//        public ActionResult GetAdminConnectionId(Guid Id)
//        {

//            var res = ChatHub.callCenterUsers.
//                                Where(x => x.UserId == Id.ToString()).FirstOrDefault();


//            if (res == null)
//            {
//                return Ok(new
//                {
//                    connectionId = ""
//                });
//            }

//            return Ok(new
//            {
//                connectionId = res.ConnectionId
//            }); 
//        }




//        [HttpGet]
//        [Route("GetByPhoneNumber")]
//        public async Task<MyAppUserResDTO> GetById(string PhoneNumber)
//        {

//            var res = await context.MyAppUsers.
//                                Where(x => x.PhoneNumber == PhoneNumber).
//                                Include(x => x.Photo).
//                                FirstOrDefaultAsync();

//            return mapper.Map<MyAppUserResDTO>(res);
//        }


//        [HttpGet]
//        [Route("GetAllUsers")]
//        public async Task<PagingDTO<MyAppUserResDTO>> GetAllUsers(int page, int pageSize)
//        {

//            var res = await context.MyAppUsers.
//                                                Where(x => x.IsDeleted == false && x.IsUser == true).
//                                                Include(x => x.Photo).
//                                                Skip((page - 1) * pageSize).
//                                                Take(pageSize).
//                                                ToListAsync();


//            return new PagingDTO<MyAppUserResDTO>()
//            {
//                Data = mapper.Map<List<MyAppUserResDTO>>(res),
//                Page = page,
//                PageSize = pageSize,
//                TotalCount = (await context.MyAppUsers.Where(x => x.IsDeleted == false && x.IsUser == true).CountAsync()),
//                CountInPage = res.Count
//            };
//        }


//        [HttpPost]
//        [Route("GetAllUsersByIds")]
//        public async Task<List<MyAppUserResDTO>> GetAllUsersByIds(List<Guid> ids)
//        {

//            var res = await context.MyAppUsers.
//                                                Where(x => x.IsDeleted == false && ids.Contains(x.Id)).
//                                                Include(x => x.Photo).
//                                                ToListAsync();


//            return mapper.Map<List<MyAppUserResDTO>>(res);
//        }
//    }
//}
