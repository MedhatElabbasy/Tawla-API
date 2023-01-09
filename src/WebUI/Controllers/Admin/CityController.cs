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
    public class CityController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public CityController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public CityResDTO Add(CityAddDTO model)
        {
            var res = mapper.Map<CityResDTO>(context.City.Add(mapper.Map<City>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<CityResDTO> Update(CityUpdateDTO model)
        {
            var setting = await context.City
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<CityResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.City
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<CityResDTO>> GetAllActive()
        {
            var res = await context.City.
                Where(x => x.IsDeleted == false && x.IsActive == true).
                ToListAsync();

            return mapper.Map<List<CityResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<CityResDTO>> GetAll()
        {
            var res = await context.City.
                Where(x => x.IsDeleted == false).
                ToListAsync();

            return mapper.Map<List<CityResDTO>>(res);
        }

    }
}
