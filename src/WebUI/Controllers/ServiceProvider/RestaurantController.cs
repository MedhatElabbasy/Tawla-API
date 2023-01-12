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
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Settings.ServiceProvider;

namespace Tawala.WebUI.Controllers.ServiceProvider
{
    public class RestaurantController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
      
        private readonly IMapper mapper;
        public RestaurantController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RestaurantResDTO Add(RestaurantAddDTO model)
        {
            var res = mapper.Map<RestaurantResDTO>(context.Restaurants.Add(mapper.Map<Restaurant>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RestaurantResDTO> Update(RestaurantUpdateDTO model)
        {
            var setting = await context.Restaurants
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<RestaurantResDTO>(setting);
        }

        [HttpPost]
        [Route("Active")]
        public async Task<RestaurantResDTO> Active(Guid Id)
        {
            var setting = await context.Restaurants
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = true;
            await context.SaveChangesAsync();
            return mapper.Map<RestaurantResDTO>(setting);
        }

        [HttpPost]
        [Route("DeActive")]
        public async Task<RestaurantResDTO> DeActive(Guid Id)
        {
            var setting = await context.Restaurants
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = false;
            await context.SaveChangesAsync();
            return mapper.Map<RestaurantResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Restaurants
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<RestaurantResDTO>> GetAllActive()
        {
            var res = await context.Restaurants.
                Where(x => x.IsDeleted == false && x.IsActive == true).
                Include(x => x.AppBaner).
                Include(x => x.City).
                Include(x => x.Branchs.Where(x => x.IsDeleted == false)).
                Include(x => x.District).
                Include(x => x.Logo).
                Include(x => x.OpenDayes.Where(x => x.IsDeleted == false)).
                Include(x => x.RestaurantType).
                ToListAsync();

            return mapper.Map<List<RestaurantResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllNotActive")]
        public async Task<List<RestaurantResDTO>> GetAllNotActive()
        {
            var res = await context.Restaurants.
                Where(x => x.IsDeleted == false && x.IsActive == false).
                Include(x => x.AppBaner).
                Include(x => x.City).
                Include(x => x.Branchs.Where(x => x.IsDeleted == false)).
                Include(x => x.District).
                Include(x => x.Logo).
                Include(x => x.OpenDayes.Where(x => x.IsDeleted == false)).
                Include(x => x.RestaurantType).
                ToListAsync();

            return mapper.Map<List<RestaurantResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<RestaurantResDTO>> GetAll()
        {
            var res = await context.Restaurants.
                Where(x => x.IsDeleted == false).
                Include(x => x.AppBaner).
                Include(x => x.City).
                Include(x => x.Branchs.Where(x => x.IsDeleted == false)).
                Include(x => x.District).
                Include(x => x.Logo).
                Include(x => x.OpenDayes.Where(x => x.IsDeleted == false)).
                Include(x => x.RestaurantType).
                ToListAsync();
            return mapper.Map<List<RestaurantResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<RestaurantResDTO> GetById(Guid Id)
        {
            var res = await context.Restaurants.
                Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.AppBaner).
                Include(x => x.City).
                Include(x => x.Branchs.Where(x => x.IsDeleted == false)).
                Include(x => x.District).
                Include(x => x.Logo).
                Include(x => x.OpenDayes.Where(x => x.IsDeleted == false)).
                Include(x => x.RestaurantType).
                ToListAsync();
            return mapper.Map<RestaurantResDTO>(res);
        }
    }
}
