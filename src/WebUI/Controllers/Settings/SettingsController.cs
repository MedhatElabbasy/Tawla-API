using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tawala.Application.Models;

using Tawala.Infrastructure.Persistence;
using AutoMapper;
using Tawala.Domain.Entities.Settings;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO;

namespace Tawala.WebUI.Controllers.SettingsController
{
    public class SettingsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public SettingsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public SettingsResDTO Add(SettingsAddDTO model)
        {
            var res = mapper.Map<SettingsResDTO>(context.Settings.Add(mapper.Map<Settings>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<SettingsResDTO> Update(SettingsUpdateDTO model)
        {
            var setting = await context.Settings
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<SettingsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Settings
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetByKey")]
        public async Task<SettingsResDTO> GetByKey(string Key)
        {
            var res = await context.Settings.Where(x => x.Key.Contains(Key) && x.IsDeleted == false).FirstOrDefaultAsync();
            return mapper.Map<SettingsResDTO>(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<SettingsResDTO>> GetAll()
        {
            var res = await context.Settings.Where(x => x.IsDeleted == false).ToListAsync();
            return mapper.Map<List<SettingsResDTO>>(res);
        }
    }
}
