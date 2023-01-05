using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.NotificationDTO;
using Tawala.Domain.Entities.Notifications;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Tawala.Application.Models.CategoryDTO;
using Tawala.Domain.Entities.Category;
using System.Linq;

namespace Tawala.WebUI.Controllers.Admin
{
    public class CategoryController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public CategoryController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public CategoryResDTO Add(CategoryAddDTO model)
        {
            var res = mapper.Map<CategoryResDTO>(context.Category.Add(mapper.Map<Category>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<CategoryResDTO> Update(CategoryUpdateDTO model)
        {
            var setting = await context.Category
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<CategoryResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Category
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAllMainCategory")]
        public async Task<List<CategoryResDTO>> GetAll()
        {
            var res = await context.Category.
                Where(x => x.IsDeleted == false && x.MainCategoryId == null).
                Include(x => x.CategoryPhoto).
                ToListAsync();

            return mapper.Map<List<CategoryResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllSubById")]
        public async Task<List<CategoryResDTO>> GetAllSubById(Guid catId)
        {
            var res = await context.Category.
                Where(x => x.IsDeleted == false && x.MainCategoryId == catId).
                Include(x => x.CategoryPhoto).
                ToListAsync();

            return mapper.Map<List<CategoryResDTO>>(res);
        }

    }
}
