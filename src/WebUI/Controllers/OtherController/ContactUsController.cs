using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO; 
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.OtherDTO;
using Tawala.Domain.Entities.Settings.Other;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.OtherController
{
    public class ContactUsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public ContactUsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public ContactUsResDTO Add(ContactUsAddDTO model)
        {
            var res = mapper.Map<ContactUsResDTO>(context.ContactUs.Add(mapper.Map<ContactUs>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ContactUsResDTO> Update(ContactUsUpdateDTO model)
        {
            var setting = await context.ContactUs
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<ContactUsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.ContactUs
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        } 
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<ContactUsResDTO>> GetAll()
        {
            var res = await context.ContactUs.Where(x => x.IsDeleted == false).ToListAsync();
            return mapper.Map<List<ContactUsResDTO>>(res);
        }
    }
}
