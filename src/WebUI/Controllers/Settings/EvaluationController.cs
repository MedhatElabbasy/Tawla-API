using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO;
using Tawala.Infrastructure.Persistence;
using Tawala.Domain.Entities.Settings.EvaluationEN;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Evaluations
{
    public class EvaluationController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public EvaluationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public EvaluationResDTO Add(EvaluationAddDTO model)
        {
            var res = mapper.Map<EvaluationResDTO>(context.Evaluations.Add(mapper.Map<Evaluation>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<EvaluationResDTO> Update(EvaluationUpdateDTO model)
        {
            var setting = await context.Evaluations
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<EvaluationResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Evaluations
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<EvaluationResDTO>> GetAll()
        {
            var res = await context.Evaluations.Where(x => x.IsDeleted == false).ToListAsync();
            return mapper.Map<List<EvaluationResDTO>>(res);
        }
    }
}
