using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Models.ReservationsDTOS;
using Tawala.Domain.Entities.Reservations;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Reservations
{
    public class OccasionsReservationController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public OccasionsReservationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OccasionsReservationResDTO Add(OccasionsReservationAddDTO model)
        {
            var res = mapper.Map<OccasionsReservationResDTO>(context.OccasionsReservation.Add(mapper.Map<OccasionsReservation>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OccasionsReservationResDTO> Update(OccasionsReservationUpdateDTO model)
        {
            var setting = await context.OccasionsReservation
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OccasionsReservationResDTO>(setting);
        }



        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.OccasionsReservation
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }


        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<OccasionsReservationResDTO>> GetAllByRestId(Guid restId)
        {
            var res = await context.OccasionsReservation.
                Where(x => x.IsDeleted == false && x.RestOccasions.RestaurantId == restId).
                Include(x => x.Branch).
                Include(x => x.Status).
                Include(x => x.RestOccasions).ThenInclude(x => x.Restaurant).ThenInclude(x => x.Logo).
                ToListAsync();
            return mapper.Map<List<OccasionsReservationResDTO>>(res);
        }


        [HttpGet]
        [Route("GetAllByRestIdandStatus")]
        public async Task<List<OccasionsReservationResDTO>> GetAllByRestIdandStatus(Guid restId, Guid status)
        {
            var res = await context.OccasionsReservation.
                Where(x => x.IsDeleted == false && x.RestOccasions.RestaurantId == restId && x.StatusId == status).
                Include(x => x.Branch).
                Include(x => x.Status).
                Include(x => x.RestOccasions).ThenInclude(x => x.Restaurant).ThenInclude(x => x.Logo).
                ToListAsync();
            return mapper.Map<List<OccasionsReservationResDTO>>(res);
        }


        [HttpGet]
        [Route("GetAllByUserId")]
        public async Task<List<OccasionsReservationResDTO>> GetAllByUserId(string userId)
        {
            var res = await context.OccasionsReservation.
                Where(x => x.IsDeleted == false && x.AppUserId == userId).
                Include(x => x.Branch).
                Include(x => x.Status).
                Include(x => x.RestOccasions).ThenInclude(x => x.Restaurant).ThenInclude(x => x.Logo).
                ToListAsync();
            return mapper.Map<List<OccasionsReservationResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<OccasionsReservationResDTO> GetById(Guid Id)
        {
            var res = await context.OccasionsReservation.
                Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.Branch).
                Include(x => x.Status).
                Include(x => x.RestOccasions).ThenInclude(x => x.Restaurant).ThenInclude(x => x.Logo).
                ToListAsync();
            return mapper.Map<OccasionsReservationResDTO>(res);
        }
    }
}
