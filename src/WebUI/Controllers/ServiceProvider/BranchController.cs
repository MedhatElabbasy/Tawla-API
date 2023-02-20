using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.ServiceProvider
{
    public class BranchController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public BranchController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public BranchResDTO Add(BranchAddDTO model)
        {
            var res = mapper.Map<BranchResDTO>(context.Branchs.Add(mapper.Map<Branch>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<BranchResDTO> Update(BranchUpdateDTO model)
        {
            var setting = await context.Branchs
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<BranchResDTO>(setting);
        }

        [HttpPost]
        [Route("Active")]
        public async Task<BranchResDTO> Active(Guid Id)
        {
            var setting = await context.Branchs
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = true;
            await context.SaveChangesAsync();
            return mapper.Map<BranchResDTO>(setting);
        }

        [HttpPost]
        [Route("DeActive")]
        public async Task<BranchResDTO> DeActive(Guid Id)
        {
            var setting = await context.Branchs
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = false;
            await context.SaveChangesAsync();
            return mapper.Map<BranchResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Branchs
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<BranchResDTO>> GetAllActive()
        {
            var res = await context.Branchs.
                Where(x => x.IsDeleted == false && x.IsActive == true).

                ToListAsync();

            return mapper.Map<List<BranchResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllNotActive")]
        public async Task<List<BranchResDTO>> GetAllNotActive()
        {
            var res = await context.Branchs.
                Where(x => x.IsDeleted == false && x.IsActive == false).

                ToListAsync();

            return mapper.Map<List<BranchResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<BranchResDTO>> GetAll()
        {
            var res = await context.Branchs.
                Where(x => x.IsDeleted == false).
                ToListAsync();
            return mapper.Map<List<BranchResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<BranchResDTO> GetById(Guid Id)
        {
            var res = await context.Branchs.
                Where(x => x.IsDeleted == false && x.Id == Id).
                FirstOrDefaultAsync();
            return mapper.Map<BranchResDTO>(res);
        }
        [HttpGet]
        [Route("GetByResId")]
        public async Task<BranchResDTO> GetByResId(Guid RestId)
        {
            var res = await context.Branchs.
                Where(x => x.IsDeleted == false && x.RestaurantId == RestId).
                FirstOrDefaultAsync();
            return mapper.Map<BranchResDTO>(res);
        }
    }
}
