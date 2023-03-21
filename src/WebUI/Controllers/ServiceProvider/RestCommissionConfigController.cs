using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Settings;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.ServiceProvider
{
    public class RestCommissionConfigController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RestCommissionConfigController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RestCommissionConfigResDTO Add(RestCommissionConfigAddDTO model)
        {
            var res = mapper.Map<RestCommissionConfigResDTO>(context.RestCommissionConfigs.Add(mapper.Map<RestCommissionConfig>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RestCommissionConfigResDTO> Update(RestCommissionConfigUpdateDTO model)
        {
            var restCommissionConfig = await context.RestCommissionConfigs
                .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            restCommissionConfig = mapper.Map(model, restCommissionConfig);
            await context.SaveChangesAsync();

            return mapper.Map<RestCommissionConfigResDTO>(restCommissionConfig);
        }

        [HttpPost]
        [Route("Activate")]
        public async Task<RestCommissionConfigResDTO> Activate(Guid Id)
        {
            var restCommissionConfig = await context.RestCommissionConfigs
                .SingleOrDefaultAsync(s => s.Id == Id);

            restCommissionConfig.IsActive = true;
            await context.SaveChangesAsync();

            return mapper.Map<RestCommissionConfigResDTO>(restCommissionConfig);
        }

        [HttpPost]
        [Route("Deactivate")]
        public async Task<RestCommissionConfigResDTO> Deactivate(Guid Id)
        {
            var restCommissionConfig = await context.RestCommissionConfigs
                .SingleOrDefaultAsync(s => s.Id == Id);

            restCommissionConfig.IsActive = false;
            await context.SaveChangesAsync();

            return mapper.Map<RestCommissionConfigResDTO>(restCommissionConfig);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var restCommissionConfig = await context.RestCommissionConfigs
                .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            restCommissionConfig.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<RestCommissionConfigResDTO>> GetAllActive()
        {
            var res = await context.RestCommissionConfigs
                .Where(x => x.IsDeleted == false && x.IsActive == true)
                 .Include(x => x.Restaurant).ThenInclude(x => x.Logo)
                .ToListAsync();

            return mapper.Map<List<RestCommissionConfigResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllNotActive")]
        public async Task<List<RestCommissionConfigResDTO>> GetAllNotActive()
        {
            var res = await context.RestCommissionConfigs
                .Where(x => x.IsDeleted == false && x.IsActive == false)
                .Include(x => x.Restaurant).ThenInclude(x=>x.Logo)
                .ToListAsync();

            return mapper.Map<List<RestCommissionConfigResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<RestCommissionConfigResDTO>> GetAll()
        {
            var res = await context.RestCommissionConfigs
                .Where(x => x.IsDeleted == false)
                 .Include(x => x.Restaurant).ThenInclude(x => x.Logo)
                .ToListAsync();

            return mapper.Map<List<RestCommissionConfigResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<RestCommissionConfigResDTO> GetById(Guid Id)
        {
            var res = await context.RestCommissionConfigs
                .Where(x => x.IsDeleted == false && x.Id == Id)
                 .Include(x => x.Restaurant).ThenInclude(x => x.Logo)
                .FirstOrDefaultAsync();

            return mapper.Map<RestCommissionConfigResDTO>(res);
        }

        [HttpGet]
        [Route("GetByRestId")]
        public async Task<RestCommissionConfigResDTO> GetByRestId(Guid RestId)
        {
            var res = await context.RestCommissionConfigs
                .Where(x => x.IsDeleted == false && x.RestaurantId == RestId)
                 .Include(x => x.Restaurant).ThenInclude(x => x.Logo)
                .FirstOrDefaultAsync();

            return mapper.Map<RestCommissionConfigResDTO>(res);
        }
    } 
}
