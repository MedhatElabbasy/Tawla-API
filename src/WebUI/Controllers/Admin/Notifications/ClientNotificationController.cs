using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.NotificationDTO;
using Tawala.Domain.Entities.Notifications;
using Tawala.Infrastructure.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tawala.WebUI.Controllers.Admin.Notifications
{

    public class ClientNotificationController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public ClientNotificationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public ClientNotificationResDTOS Add(ClientNotificationAddDTOS model)
        {
            var res = mapper.Map<ClientNotificationResDTOS>(context.ClientNotifications.Add(mapper.Map<ClientNotification>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ClientNotificationResDTOS> Update(ClientNotificationUpdateDTOS model)
        {
            var setting = await context.ClientNotifications
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<ClientNotificationResDTOS>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.ClientNotifications
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<ClientNotificationResDTOS>> GetAll()
        {
            var res = await context.ClientNotifications.Where(x => x.IsDeleted == false).
                Include(x => x.NotificationPhoto).
                ToListAsync();
            return mapper.Map<List<ClientNotificationResDTOS>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ClientNotificationResDTOS> GetById(Guid id)
        {
            var res = await context.ClientNotifications.Where(x => x.IsDeleted == false && x.Id == id).
                Include(x => x.NotificationPhoto).
                FirstOrDefaultAsync();
            return mapper.Map<ClientNotificationResDTOS>(res);
        }


    }
}
