using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Domain.Entities.menu;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.ItemsDTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.MenuControllers
{
    public class MenuController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public MenuController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public MenuResDTO Add(MenuAddDTO model)
        {
            var res = mapper.Map<MenuResDTO>(context.Menus.Add(mapper.Map<Menu>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<MenuResDTO> Update(MenuUpdateDTO model)
        {
            var setting = await context.Menus
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<MenuResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.Menus
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        }

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<MenuResDTO>> GetAllByRestId(Guid resId)
        {
            var res = await context.Menus.Where(x => x.IsDeleted == false && x.RestaurantId == resId).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.MenuItems).
                ThenInclude(x => x.Items).
                ThenInclude(x => x.Category).
                ToListAsync();
            return mapper.Map<List<MenuResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<List<MenuResDTO>> GetById(Guid Id)
        {
            var res = await context.Menus.Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.MenuItems).
                ThenInclude(x => x.Items).
                ThenInclude(x => x.Category).
                ToListAsync();
            return mapper.Map<List<MenuResDTO>>(res);
        }
    }
}
