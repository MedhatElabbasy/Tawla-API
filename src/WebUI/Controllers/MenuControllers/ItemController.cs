using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.ItemsDTOS;
using Tawala.Domain.Entities.menu;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.MenuControllers
{
    public class ItemsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public ItemsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public ItemsResDTO Add(ItemsAddDTO model)
        {
            var res = mapper.Map<ItemsResDTO>(context.Items.Add(mapper.Map<Items>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ItemsResDTO> Update(ItemsUpdateDTO model)
        {
            var setting = await context.Items
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<ItemsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.Items
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true; 
            return await context.SaveChangesAsync() > 0;
             
        }

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<ItemsResDTO>> GetAllByRestId(Guid resId)
        {
            var res = await context.Items.Where(x => x.IsDeleted == false && x.RestaurantId == resId).
                Include(x => x.Restaurant).
                Include(x => x.Category).
                ToListAsync();
            return mapper.Map<List<ItemsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllByRestIdandCategory")]
        public async Task<List<ItemsResDTO>> GetAllByRestIdandCategory(Guid resId, Guid catId)
        {
            var res = await context.Items.Where(x => x.IsDeleted == false && x.RestaurantId == resId && x.CategoryId == catId).
                Include(x => x.Restaurant).
                Include(x => x.Category).
                ToListAsync();
            return mapper.Map<List<ItemsResDTO>>(res);
        }

    }
}
