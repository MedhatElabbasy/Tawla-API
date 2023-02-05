using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.ItemsDTOS;
using Tawala.Infrastructure.Persistence;
using Tawala.Domain.Entities.menu;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.MenuItemsControllers
{
    public class MenuItemsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public MenuItemsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public MenuItemsResDTO Add(MenuItemsAddDTO model)
        {
            var res = mapper.Map<MenuItemsResDTO>(context.MenuItems.Add(mapper.Map<MenuItems>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<MenuItemsResDTO> Update(MenuItemsUpdateDTO model)
        {
            var setting = await context.MenuItems
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<MenuItemsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {
            var productInDb = await context.MenuItems
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;

        }

        //[HttpGet]
        //[Route("GetAllByMenuId")]
        //public async Task<List<MenuItemsResDTO>> GetAllByRestId(Guid menuId)
        //{
        //    var res = await context.MenuItems.Where(x => x.IsDeleted == false && x.MenuId == menuId).
        //        Include(x => x.Menu).
        //        Include(x => x.Items).
        //        ToListAsync();
        //    return mapper.Map<List<MenuItemsResDTO>>(res);
        //}

        [HttpGet]
        [Route("GetById")]
        public async Task<List<MenuItemsResDTO>> GetById(Guid Id)
        {
            var res = await context.MenuItems.Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.Menu).
                Include(x => x.Items).
                ToListAsync();
            return mapper.Map<List<MenuItemsResDTO>>(res);
        }
    }
}
