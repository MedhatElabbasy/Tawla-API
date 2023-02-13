using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.TawalhDesignWithConfigDTO;
using Tawala.Domain.Entities.TawalhDesignWithConfig;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Org.BouncyCastle.Ocsp;

namespace Tawala.WebUI.Controllers.TawalhDesignWithConfig
{
    public class FloorController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public FloorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public FloorResDTO Add(FloorAddDTO model)
        {
            var res = mapper.Map<FloorResDTO>(context.Floors.Add(mapper.Map<Floor>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<FloorResDTO> Update(FloorUpdateDTO model)
        {
            var setting = await context.Floors
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<FloorResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.Floors
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        }

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<FloorResDTO>> GetAllByRestId(Guid resId)
        {
            var res = await context.Floors.Where(x => x.IsDeleted == false && x.Building.RestaurantId == resId).
                Include(x => x.Building).
                ThenInclude(x => x.BuildingType).
                Include(x => x.FloorType).
                ToListAsync();
            return mapper.Map<List<FloorResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllByBuildingId")]
        public async Task<List<FloorResDTO>> GetAllByBuildingId(Guid BuildingId)
        {
            var res = await context.Floors.Where(x => x.IsDeleted == false && x.BuildingId == BuildingId).
                Include(x => x.Building).
                ThenInclude(x => x.BuildingType).
                Include(x => x.FloorType).
                ToListAsync();
            return mapper.Map<List<FloorResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllById")]
        public async Task<FloorResDTO> GetAllById(Guid Id)
        {
            var res = await context.Floors.Where(x => x.IsDeleted == false && x.Id == Id).
               Include(x => x.Building).
               ThenInclude(x => x.BuildingType).
               Include(x => x.FloorType).
               FirstOrDefaultAsync();
            return mapper.Map<FloorResDTO>(res);
        }
    }
}
