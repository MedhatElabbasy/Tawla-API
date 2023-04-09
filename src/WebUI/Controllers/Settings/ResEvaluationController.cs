using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.ResEvaluationDTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tawala.Domain.Entities.Settings.ResEvaluation;

namespace Tawala.WebUI.Controllers.ResEvaluationController
{
    public class ResEvaluationController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public ResEvaluationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("ResEvaluation")]
        public async Task<ResEvaluationResDTO> ResEvaluation(ResEvaluationAddDTO model)
        {

            var resEvaluation = await context.ResEvaluations
                         .SingleOrDefaultAsync(s => s.RestaurantId == model.RestaurantId && s.IsDeleted == false && model.AppUserId == model.AppUserId);
            if (resEvaluation != null)
            {
                var setting = await context.ResEvaluations
                         .SingleOrDefaultAsync(s => s.RestaurantId == model.RestaurantId && s.IsDeleted == false && model.AppUserId == model.AppUserId);

                setting = mapper.Map(model, setting);
                await context.SaveChangesAsync();
                return mapper.Map<ResEvaluationResDTO>(setting);

            }


            var res = mapper.Map<ResEvaluationResDTO>(context.ResEvaluations.Add(mapper.Map<ResEvaluation>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        //[HttpPost]
        //[Route("Update")]
        //public async Task<ResEvaluationResDTO> Update(ResEvaluationUpdateDTO model)
        //{
        //    var setting = await context.ResEvaluations
        //                  .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

        //    setting = mapper.Map(model, setting);
        //    await context.SaveChangesAsync();
        //    return mapper.Map<ResEvaluationResDTO>(setting);
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<bool> Delete(Guid Id)
        //{

        //    var productInDb = await context.ResEvaluations
        //                    .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

        //    productInDb.IsDeleted = true;

        //    return await context.SaveChangesAsync() > 0;

        //}

        [HttpGet]
        [Route("GetAllByRestId")]
        public async Task<List<ResEvaluationResDTO>> GetAllByRestId(Guid RestId)
        {
            var res = await context.ResEvaluations.
                Where(x => x.IsDeleted == false && x.RestaurantId == RestId).
                ToListAsync();
            return mapper.Map<List<ResEvaluationResDTO>>(res);
        }

        [HttpGet]
        [Route("GetAllByRestIdandUserId")]
        public async Task<List<ResEvaluationResDTO>> GetAllByRestIdandUserId(Guid RestId, string userId)
        {
            var res = await context.ResEvaluations.
                Where(x => x.IsDeleted == false && x.RestaurantId == RestId && x.AppUserId == userId).
                ToListAsync();
            return mapper.Map<List<ResEvaluationResDTO>>(res);
        }
    }
}
