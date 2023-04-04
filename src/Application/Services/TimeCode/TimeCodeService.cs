using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Common.DateTimeCalculation;
using Tawala.Application.Common.Exceptions;
using Tawala.Domain.Entities.Identity;
using Tawala.Infrastructure.Repository;

namespace Tawala.Application.Services.TimeCode
{
    public class TimeCodeService : ITimeCodeService
    {
        private readonly IRepositoryBase<TempCode> repository;
        private readonly IMapper mapper;
        private readonly IEncryptDecryptService encryptDecryptService;
        public TimeCodeService(
            IRepositoryBase<TempCode> _repository,
            IMapper _mapper,
            IEncryptDecryptService _encryptDecryptService)
        {
            this.repository = _repository;
            this.mapper = _mapper;
            this.encryptDecryptService = _encryptDecryptService;
        }


        public async Task<string> AddTimeCode(TempCode timeCode)
        {
            //drop old code
            await repository.ExecuteSql("delete from TempCodes where AppUserId='" + timeCode.AppUserId + "'");
            //getnerate code
            var code = GenerateTimeCode();
            //Encrypt code
            timeCode.TempCodeHash = encryptDecryptService.Encrypt(code);
            timeCode.CreatedAt = DateTime.Now;
            timeCode.ExpiredAt = timeCode.CreatedAt.AddMinutes(2);
            var res = await repository.Add(timeCode);
            if (res == null)
            {
                throw new GlobalException(exMessage: "حدث خطأ");
            }
            else
            {
                return code;
            }
        }

        public string GenerateTimeCode()
        {
            Random rnd = new Random();
            int one = rnd.Next(1, 9);  // creates a number between 1 and 9
            int two = rnd.Next(10, 90);   // creates a number between 10 and 6
            int three = rnd.Next(100, 999);   // creates a number between 100 and 999
            return one.ToString() + two.ToString() + three.ToString();
        }

        public async Task<int> Validation(string code, string aspNetUser)
        {
            var encryptedTimeCode = repository.Find(x => x.IsDeleted == false).
                                            Where(x => x.AppUserId == aspNetUser).
                                            OrderByDescending(x => x.CreatedAt).
                                            FirstOrDefault();

            if (encryptedTimeCode == null)
            {
                //not found code
                return -1;
            }
            if (encryptedTimeCode.TempCodeHash == encryptDecryptService.Encrypt(code))
            {
                if (DateTime.Now.IsBewteenTwoDates(encryptedTimeCode.CreatedAt, encryptedTimeCode.ExpiredAt))
                {
                    //drop old code
                    await repository.ExecuteSql("delete from TempCodes where AppUserId='" + aspNetUser + "'");
                    return 1;
                }
                else
                {
                    //code is expired
                    return 0;
                }
            }
            else
            {
                //code is invalid
                return -2;
            }
        }
    }
}

