using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Microsoft.AspNetCore.Mvc;

namespace Tawala.WebUI.Controllers.Admin.OpstionSet
{
    public class OptionSetController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public OptionSetController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OptionSetResDTO Add(OptionSetAddDTO model)
        {
            var res = mapper.Map<OptionSetResDTO>(context.OptionSet.Add(mapper.Map<OptionSet>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OptionSetResDTO> Update(OptionSetUpdateDTO model)
        {
            var setting = await context.OptionSetItems
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OptionSetResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.OptionSet
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<OptionSetResDTO>> GetAll()
        {
            var res = await context.OptionSet.
                Where(x => x.IsDeleted == false).
                Include(x => x.OptionSetItems.Where(x => x.IsDeleted == false)).
                ToListAsync();

            return mapper.Map<List<OptionSetResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<OptionSetResDTO> GetById(Guid Id)
        {
            var res = await context.OptionSet.
                Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.OptionSetItems.Where(x => x.IsDeleted == false)).
                FirstOrDefaultAsync();

            return mapper.Map<OptionSetResDTO>(res);
        } 
    }
}
