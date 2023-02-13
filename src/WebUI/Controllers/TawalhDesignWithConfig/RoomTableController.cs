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
    public class RoomTableController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public RoomTableController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RoomTableResDTO Add(RoomTableAddDTO model)
        {
            var res = mapper.Map<RoomTableResDTO>(context.RoomTables.Add(mapper.Map<RoomTable>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RoomTableResDTO> Update(RoomTableUpdateDTO model)
        {
            var setting = await context.RoomTables
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<RoomTableResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.RoomTables
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        } 
       
        [HttpGet]
        [Route("GetAllById")]
        public async Task<RoomTableResDTO> GetAllById(Guid Id)
        {
            var res = await context.RoomTables.Where(x => x.Id == Id && x.IsDeleted == false).

                FirstOrDefaultAsync();
            return mapper.Map<RoomTableResDTO>(res);
        }
    }
}
