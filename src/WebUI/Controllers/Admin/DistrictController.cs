using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO.AdminDTOS;
using Tawala.Domain.Entities.Settings.AdminSettings;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Admin
{
    public class DistrictController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public DistrictController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public DistrictResDTO Add(DistrictAddDTO model)
        {
            var res = mapper.Map<DistrictResDTO>(context.District.Add(mapper.Map<District>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<DistrictResDTO> Update(DistrictUpdateDTO model)
        {
            var setting = await context.District
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<DistrictResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.District
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<DistrictResDTO>> GetAllActive()
        {
            var res = await context.District.
                Where(x => x.IsDeleted == false && x.IsActive == true).
                ToListAsync();

            return mapper.Map<List<DistrictResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<DistrictResDTO>> GetAll()
        {
            var res = await context.District.
                Where(x => x.IsDeleted == false).
                ToListAsync();

            return mapper.Map<List<DistrictResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<DistrictResDTO> GetAll(Guid Id)
        {
            var res = await context.District.
                Where(x => x.IsDeleted == false && x.Id == Id).
                FirstOrDefaultAsync();
            return mapper.Map<DistrictResDTO>(res);
        }


        [HttpGet]
        [Route("GetAllByCityId")]
        public async Task<List<DistrictResDTO>> GetAllByCityId(long Id)
        {
            var res = await context.District.
                Where(x => x.IsDeleted == false && x.CityId == Id && x.IsActive == true).
                ToListAsync();
            return mapper.Map<List<DistrictResDTO>>(res);
        }

    }
}
