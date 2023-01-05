//using Tawala.Application.Common.Mappings;
//using Tawala.Domain.Entities.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tawala.Application.Models.Auth.Users
//{
//    public class UserAddDTO : IMapFrom<AppUser>
//    {
//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string LastName { get; set; }
//        public string UserName { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string PhoneNumber { get; set; }
//        public bool IsActive { get; set; }
//        public string OtherPhoneNumber { get; set; }
//        public string Address { get; set; }
//        public bool IsUser { get; set; }
//        public bool IsAdmin { get; set; }
//    }

//    public class UserUpdateDTO : IMapFrom<AppUser>
//    {
//        public string Id { get; set; }
//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string LastName { get; set; }
//        public string UserName { get; set; }
//        public string Email { get; set; }
//        public string PhoneNumber { get; set; }
//        public string OtherPhoneNumber { get; set; }
//        public string Address { get; set; }
//        public bool IsUser { get; set; }
//        public bool IsAdmin { get; set; }
//    }
//    public class UserResDTO : IMapFrom<AppUser>
//    {
//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string LastName { get; set; }
//        public string UserName { get; set; }
//        public string Email { get; set; }
//        public string PhoneNumber { get; set; }
//        public bool IsActive { get; set; }
//        public bool IsValidUser { get; set; }
//        public string Message { get; set; }
//        public string OtherPhoneNumber { get; set; }
//        public string Address { get; set; }
//        public bool IsUser { get; set; }
//        public bool IsAdmin { get; set; }
//    }

//    public class UserLoginDTO
//    {
//        public string UserName { get; set; }
//        public string Password { get; set; }
//    }

//    public class RegisterResult
//    {
//        public string Status { get; set; }
//        public string Message { get; set; }
//        public string Code { get; set; }
//    }
//    public class LoginResult
//    {
//        public string Status { get; set; }
//        public string Message { get; set; }
//        public string Code { get; set; }
//    }
//}
