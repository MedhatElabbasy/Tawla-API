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

namespace Tawala.WebUI.Controllers.TawalhDesignWithConfig
{
    public class RoomController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public RoomController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RoomResDTO Add(RoomAddDTO model)
        {
            var res = mapper.Map<RoomResDTO>(context.Rooms.Add(mapper.Map<Room>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RoomResDTO> Update(RoomUpdateDTO model)
        {
            var setting = await context.Rooms
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<RoomResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.Rooms
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        }

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<RoomResDTO>> GetAllByRestId(Guid resId)
        {
            var res = await context.Rooms.
                Where(x => x.IsDeleted == false && x.Floor.Building.RestaurantId == resId).
              
                ToListAsync();
            return mapper.Map<List<RoomResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllByFloorId")]
        public async Task<List<RoomResDTO>> GetAllByFloorId(Guid FloorId)
        {
            var res = await context.Rooms.
                Where(x => x.IsDeleted == false && x.FloorId == FloorId).
                 Include(x => x.Floor).ThenInclude(x => x.FloorType).
                 Include(x => x.Floor).ThenInclude(x => x.Building).ThenInclude(x => x.BuildingType).
                 Include(x => x.Floor).ThenInclude(x => x.Building).ThenInclude(x => x.Restaurant).
                ToListAsync();
            return mapper.Map<List<RoomResDTO>>(res);
        }


        [HttpGet]
        [Route("GetAllById")]
        public async Task<RoomResDTO> GetAllById(Guid Id)
        {
            var res = await context.Rooms.Where(x => x.Id == Id && x.IsDeleted == false).
                 Include(x => x.Floor).ThenInclude(x=>x.FloorType). 
                 Include(x => x.Floor).ThenInclude(x=>x.Building).ThenInclude(x=>x.BuildingType). 
                 Include(x => x.Floor).ThenInclude(x=>x.Building).ThenInclude(x=>x.Restaurant).
                
                FirstOrDefaultAsync();
            return mapper.Map<RoomResDTO>(res);
        }
    }
}
