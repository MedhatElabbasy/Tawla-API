using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt; 
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.SettingDTO.AdminDTOS;
using Tawala.Domain.Entities.Settings.AdminSettings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Admin
{
    public class RegionsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public RegionsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RegionsResDTO Add(RegionsAddDTO model)
        {
            var res = mapper.Map<RegionsResDTO>(context.Regions.Add(mapper.Map<Regions>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RegionsResDTO> Update(RegionsUpdateDTO model)
        {
            var setting = await context.Regions
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<RegionsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Regions
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<RegionsResDTO>> GetAllActive()
        {
            var res = await context.Regions.
                Where(x => x.IsDeleted == false && x.IsActive == true).
                ToListAsync();

            return mapper.Map<List<RegionsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<RegionsResDTO>> GetAll()
        {
            var res = await context.Regions.
                Where(x => x.IsDeleted == false).
                ToListAsync();

            return mapper.Map<List<RegionsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<RegionsResDTO> GetAll(Guid Id)
        {
            var res = await context.Regions.
                Where(x => x.IsDeleted == false && x.Id == Id).
                FirstOrDefaultAsync();
            return mapper.Map<RegionsResDTO>(res);
        }

    }
}
