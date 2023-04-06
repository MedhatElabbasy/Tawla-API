using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.OffersDTO;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tawala.Domain.Entities;

namespace Tawala.WebUI.Controllers
{
    public class OffersController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public OffersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OffersResDTO Create(OffersAddDTO model)
        {
            var res = mapper.Map<OffersResDTO>(context.Offers.Add(mapper.Map<Offers>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OffersResDTO> Update(OffersUpdateDTO model)
        {
            var setting = await context.Offers
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OffersResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Offers
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<OffersResDTO>> GetAll()
        {
            var res = await context.Offers.Where(x => x.IsDeleted == false).ToListAsync();
            return mapper.Map<List<OffersResDTO>>(res);
        }
    }
}
