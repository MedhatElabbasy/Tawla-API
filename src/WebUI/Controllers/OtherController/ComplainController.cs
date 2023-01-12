using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Models.ServiceProviderDTO;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.OtherDTO;
using Tawala.Domain.Entities.Settings.Other;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.OtherController
{
    public class ComplainController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public ComplainController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public ComplainResDTO Add(ComplainAddDTO model)
        {
            var res = mapper.Map<ComplainResDTO>(context.Complains.Add(mapper.Map<Complain>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ComplainResDTO> Update(ComplainUpdateDTO model)
        {
            var setting = await context.Complains
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<ComplainResDTO>(setting);
        }


        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Complains
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<List<ComplainResDTO>> GetAll()
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ComplainResDTO> GetById(Guid Id)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                FirstOrDefaultAsync();
            return mapper.Map<ComplainResDTO>(res);
        }

        [HttpGet]
        [Route("GetByRestaurantId")]
        public async Task<List<ComplainResDTO>> GetByRestaurantId(Guid Id)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.RestaurantId == Id).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                  ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetByBranchId")]
        public async Task<List<ComplainResDTO>> GetByBranchId(Guid Id)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.BranchId == Id).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                  ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetByRestaurantAndStatusId")]
        public async Task<List<ComplainResDTO>> GetByRestaurantAndStatusId(Guid Id, Guid StatusId)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.RestaurantId == Id && x.ComplainStatusId == StatusId).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                  ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetByRestaurantAndComplainTypeId")]
        public async Task<List<ComplainResDTO>> GetByRestaurantAndComplainTypeId(Guid Id, Guid StatusId)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.RestaurantId == Id && x.ComplainTypeId == StatusId).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                  ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetByBranchAndStatusId")]
        public async Task<List<ComplainResDTO>> GetByBranchAndStatusId(Guid Id, Guid StatusId)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.BranchId == Id && x.ComplainStatusId == StatusId).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                 ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }

        [HttpGet]
        [Route("GetByBranchAndComplainTypeId")]
        public async Task<List<ComplainResDTO>> GetByBranchAndComplainTypeId(Guid Id, Guid StatusId)
        {
            var res = await context.Complains.
                Where(x => x.IsDeleted == false && x.BranchId == Id && x.ComplainTypeId == StatusId).
                Include(x => x.Restaurant).ThenInclude(x => x.Logo).
                Include(x => x.ComplainStatus).
                Include(x => x.ComplainType).
                Include(x => x.Branch).
                Include(x => x.ComplainUser).
                ToListAsync();
            return mapper.Map<List<ComplainResDTO>>(res);
        }
    }
}
