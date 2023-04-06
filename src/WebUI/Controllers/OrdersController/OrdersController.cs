using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.OtherDTO;
using Tawala.Domain.Entities.Settings.Other;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.OrdersDTO;
using Tawala.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.OrdersController
{
    public class OrdersController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public OrdersResDTO Create(OrdersAddDTO model)
        {
            var res = mapper.Map<OrdersResDTO>(context.Orders.Add(mapper.Map<Orders>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<OrdersResDTO> Update(OrdersUpdateDTO model)
        {
            var setting = await context.Orders
                          .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

            setting = mapper.Map(model, setting);
            await context.SaveChangesAsync();
            return mapper.Map<OrdersResDTO>(setting);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Orders
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;


        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<OrdersResDTO>> GetAll()
        {
            var res = await context.Orders.Where(x => x.IsDeleted == false).ToListAsync();
            return mapper.Map<List<OrdersResDTO>>(res);
        }
    }
}
