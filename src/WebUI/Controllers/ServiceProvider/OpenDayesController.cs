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
    public class OpenDayesController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public OpenDayesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OpenDayesResDTO Add(OpenDayesAddDTO model)
        {
            var res = mapper.Map<OpenDayesResDTO>(context.OpenDayes.Add(mapper.Map<OpenDayes>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OpenDayesResDTO> Update(OpenDayesUpdateDTO model)
        {
            var setting = await context.OpenDayes
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OpenDayesResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.OpenDayes
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<List<OpenDayesResDTO>> GetAll()
        {
            var res = await context.OpenDayes.
                Where(x => x.IsDeleted == false).
                ToListAsync();
            return mapper.Map<List<OpenDayesResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<OpenDayesResDTO> GetById(Guid Id)
        {
            var res = await context.OpenDayes.
                Where(x => x.IsDeleted == false && x.Id == Id).
                FirstOrDefaultAsync();
            return mapper.Map<OpenDayesResDTO>(res);



        }

        [HttpGet]
        [Route("GetByRestId")]
        public async Task<List<OpenDayesResDTO>> GetByRestId(Guid Id)
        {
            var res = await context.OpenDayes.
                Where(x => x.IsDeleted == false && x.RestaurantId == Id).
                ToListAsync();
            return mapper.Map<List<OpenDayesResDTO>>(res);
        }
    }
}
