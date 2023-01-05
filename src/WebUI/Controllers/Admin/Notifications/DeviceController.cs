using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Tawala.Domain.Entities.Notifications;
using Tawala.Application.Models.NotificationDTO;
using System.Linq; 

namespace Tawala.WebUI.Controllers.Admin.Notifications
{
    public class DeviceController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public DeviceController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<DeviceResDTO> Add(DeviceAddDTO model)
        {
            //check if exist 
            var device = await context.Device.Where(x => x.DeviceToken == model.DeviceToken).FirstOrDefaultAsync();
            if (device == null)
            {
                var res = mapper.Map<DeviceResDTO>(context.Device.Add(mapper.Map<Device>(model)).Entity);
                context.SaveChanges();
                return res;
            }
            else
            {

                var setting = await context.Device
                        .SingleOrDefaultAsync(s => s.DeviceToken == model.DeviceToken && s.IsDeleted == false);

                setting = mapper.Map(model, setting);
                await context.SaveChangesAsync();
                return mapper.Map<DeviceResDTO>(setting);
            }

        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Device
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }


    }
}
