using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Infrastructure.Persistence;
using Tawala.Application.Models.OrdersDTO;
using Tawala.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tawala.Infrastructure.Common.Models;
using Tawala.Domain.Entities.Settings;

namespace Tawala.WebUI.Controllers.OrderItemsController
{
    public class OrderItemsController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public OrderItemsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<OrderItemsResDTO> AddToCart(OrderItemsAddDTO model)
        {
            var itemdata = await context.Items.Where(x => x.Id == model.ItemsId).FirstOrDefaultAsync();
            model.ItemPrice = itemdata.Price;
            model.TotalWithAmount = itemdata.Price * model.Amount;

            //check is take befor
            var OrderItems = await context.OrderItems
                          .SingleOrDefaultAsync(s => s.ItemsId == model.ItemsId && s.IsDeleted == false && s.AppUserId == model.AppUserId);

            if (OrderItems != null)
            {
                var itemdata2 = await context.Items.Where(x => x.Id == model.ItemsId).FirstOrDefaultAsync();
                model.ItemPrice = itemdata2.Price;
                model.TotalWithAmount = itemdata2.Price * model.Amount;

                OrderItems = mapper.Map(model, OrderItems);
                await context.SaveChangesAsync();
                return mapper.Map<OrderItemsResDTO>(OrderItems);
            }
            else
            {
                var res = mapper.Map<OrderItemsResDTO>(context.OrderItems.Add(mapper.Map<OrderItems>(model)).Entity);
                context.SaveChanges();
                return res;
            }
        }

        //[HttpPost]
        //[Route("Update")]
        //public async Task<OrderItemsResDTO> Update(OrderItemsUpdateDTO model)
        //{
        //    var setting = await context.OrderItems
        //                  .SingleOrDefaultAsync(s => s.Id == model.Id && s.IsDeleted == false);

        //    var itemdata = await context.Items.Where(x => x.Id == model.ItemsId).FirstOrDefaultAsync();
        //    model.ItemPrice = itemdata.Price;
        //    model.TotalWithAmount = itemdata.Price * model.Amount;

        //    setting = mapper.Map(model, setting);
        //    await context.SaveChangesAsync();
        //    return mapper.Map<OrderItemsResDTO>(setting);
        //}

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.OrderItems
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0;
        }

        [HttpGet]
        [Route("GetAllByUserId")]
        public async Task<List<OrderItemsResDTO>> GetAllByUserId(string userId)
        {
            var res = await context.OrderItems.Where(x => x.IsDeleted == false && x.AppUserId == userId).
                Include(x => x.Items).ThenInclude(x => x.Photo).
                Include(x => x.Items).ThenInclude(x => x.Restaurant).ThenInclude(x => x.Logo).
                ToListAsync();
            return mapper.Map<List<OrderItemsResDTO>>(res);
        }

        [HttpGet]
        [Route("CheckSameRestaurant")]
        public async Task<ActionResult> CheckSameRestaurant(string userId, Guid restId)
        {
            var res = await context.OrderItems.Where(x => x.IsDeleted == false && x.AppUserId == userId && x.OrdersId == null).

                ToListAsync();

            var res2 = await context.OrderItems.Where(x => x.IsDeleted == false && x.AppUserId == userId && x.Items.RestaurantId == restId && x.OrdersId == null).

            ToListAsync();

            if (res.Count == 0)
            {
                return Ok(new
                {
                    Result = true
                });
            }
            else if (res.Count == res2.Count)
            {
                return Ok(new
                {
                    Result = true
                });
            }
            else
            {
                return Ok(new
                {
                    Result = false
                });
            }
        }
    }
}
