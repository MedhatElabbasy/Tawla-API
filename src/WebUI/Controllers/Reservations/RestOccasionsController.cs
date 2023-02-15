using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System; 
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.ReservationsDTOS;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tawala.Domain.Entities.Reservations;

namespace Tawala.WebUI.Controllers.Reservations
{
    public class RestOccasionsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public RestOccasionsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public RestOccasionsResDTO Add(RestOccasionsAddDTO model)
        {
            var res = mapper.Map<RestOccasionsResDTO>(context.RestOccasions.Add(mapper.Map<RestOccasions>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<RestOccasionsResDTO> Update(RestOccasionsUpdateDTO model)
        {
            var setting = await context.RestOccasions
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<RestOccasionsResDTO>(setting);
        }

        [HttpPost]
        [Route("Active")]
        public async Task<RestOccasionsResDTO> Active(Guid Id)
        {
            var setting = await context.RestOccasions
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = true;
            await context.SaveChangesAsync();
            return mapper.Map<RestOccasionsResDTO>(setting);
        }

        [HttpPost]
        [Route("DeActive")]
        public async Task<RestOccasionsResDTO> DeActive(Guid Id)
        {
            var setting = await context.RestOccasions
                          .SingleOrDefaultAsync(s => s.Id == Id);

            setting.IsActive = false;
            await context.SaveChangesAsync();
            return mapper.Map<RestOccasionsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.RestOccasions
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<List<RestOccasionsResDTO>> GetAllActive()
        {
            var res = await context.RestOccasions.
                Where(x => x.IsDeleted == false && x.IsActive == true).

                ToListAsync();

            return mapper.Map<List<RestOccasionsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllNotActive")]
        public async Task<List<RestOccasionsResDTO>> GetAllNotActive()
        {
            var res = await context.RestOccasions.
                Where(x => x.IsDeleted == false && x.IsActive == false).

                ToListAsync();

            return mapper.Map<List<RestOccasionsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<RestOccasionsResDTO>> GetAll()
        {
            var res = await context.RestOccasions.
                Where(x => x.IsDeleted == false).
                ToListAsync();
            return mapper.Map<List<RestOccasionsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<RestOccasionsResDTO> GetById(Guid Id)
        {
            var res = await context.RestOccasions.
                Where(x => x.IsDeleted == false && x.Id == Id).
                FirstOrDefaultAsync();
            return mapper.Map<RestOccasionsResDTO>(res);
        }
    }
}
