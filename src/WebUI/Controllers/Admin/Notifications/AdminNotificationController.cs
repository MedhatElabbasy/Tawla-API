using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.SettingDTO;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.NotificationDTO;
using Tawala.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.Admin.Notifications
{
    public class AdminNotificationController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public AdminNotificationController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public AdminNotificationsResDTO Add(AdminNotificationsAddDTO model)
        {
            var res = mapper.Map<AdminNotificationsResDTO>(context.AdminNotifications.Add(mapper.Map<AdminNotifications>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<AdminNotificationsResDTO> Update(AdminNotificationsUpdateDTO model)
        {
            var setting = await context.AdminNotifications
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<AdminNotificationsResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.AdminNotifications
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<AdminNotificationsResDTO>> GetAll()
        {
            var res = await context.AdminNotifications.Where(x => x.IsDeleted == false).
                Include(x => x.NotificationPhoto).
                ToListAsync();
            return mapper.Map<List<AdminNotificationsResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<AdminNotificationsResDTO> GetById(Guid id)
        {
            var res = await context.AdminNotifications.Where(x => x.IsDeleted == false && x.Id == id).
                Include(x => x.NotificationPhoto).
                FirstOrDefaultAsync();
            return mapper.Map<AdminNotificationsResDTO>(res);
        }

    }
}
