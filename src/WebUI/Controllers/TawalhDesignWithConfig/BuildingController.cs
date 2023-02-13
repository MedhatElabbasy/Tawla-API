using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Domain.Entities.menu;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.TawalhDesignWithConfigDTO;
using Tawala.Domain.Entities.TawalhDesignWithConfig;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.TawalhDesignWithConfig
{
    public class BuildingController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public BuildingController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public BuildingResDTO Add(BuildingAddDTO model)
        {
            var res = mapper.Map<BuildingResDTO>(context.Buildings.Add(mapper.Map<Building>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<BuildingResDTO> Update(BuildingUpdateDTO model)
        {
            var setting = await context.Buildings
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<BuildingResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.Buildings
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        }

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<BuildingResDTO>> GetAllByRestId(Guid resId)
        {
            var res = await context.Buildings.Where(x => x.IsDeleted == false && x.RestaurantId == resId).
                Include(x => x.Restaurant).
                Include(x => x.BuildingType).
                Include(x => x.Branch).
                ToListAsync();
            return mapper.Map<List<BuildingResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllById")]
        public async Task<BuildingResDTO> GetAllById(Guid Id)
        {
            var res = await context.Buildings.Where(x => x.Id == Id && x.IsDeleted == false).
                Include(x => x.Restaurant).
                Include(x => x.BuildingType).
                Include(x => x.Branch).
                FirstOrDefaultAsync();
            return mapper.Map<BuildingResDTO>(res);
        }
    }
}
