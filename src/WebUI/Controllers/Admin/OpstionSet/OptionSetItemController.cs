using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Admin.OpstionSet
{
    public class OptionSetItemController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public OptionSetItemController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OptionSetItemResDTO Add(OptionSetItemAddDTO model)
        {
            var res = mapper.Map<OptionSetItemResDTO>(context.OptionSetItem.Add(mapper.Map<OptionSetItem>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OptionSetItemResDTO> Update(OptionSetItemUpdateDTO model)
        {
            var setting = await context.OptionSetItem
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OptionSetItemResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.OptionSetItem
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<OptionSetItemResDTO>> GetAll()
        {
            var res = await context.OptionSetItem.
                Where(x => x.IsDeleted == false).
                Include(x => x.OptionSet).
                ToListAsync();

            return mapper.Map<List<OptionSetItemResDTO>>(res);
        }



        [HttpGet]
        [Route("GetById")]
        public async Task<OptionSetItemResDTO> GetById(Guid Id)
        {
            var res = await context.OptionSetItem.
                Where(x => x.IsDeleted == false && x.Id == Id).
              Include(x => x.OptionSet).
                FirstOrDefaultAsync();

            return mapper.Map<OptionSetItemResDTO>(res);
        }
    }
}
