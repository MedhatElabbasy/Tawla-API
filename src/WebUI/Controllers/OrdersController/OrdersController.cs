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
        [Route("Create")]
        public async Task<ActionResult> Create(string appuserId, string Code)
        {
            var orderItems = await context.OrderItems.
                                                    Where(x => x.AppUserId == appuserId && x.OrdersId == null).
                                                    Include(x => x.Items).ThenInclude(x => x.Restaurant).
                                                    ToListAsync();

            if (orderItems.Count == 0)
            {
                return Ok(new
                {
                    message = "No item selected"
                });

            }
            //get offer
            var offer = await context.Offers.
                Where(x => x.Code == Code && x.IsActive == true && (x.StartDate.Date <= DateTime.Now.Date && x.EndDate.Date >= DateTime.Now.Date)).
                FirstOrDefaultAsync();
            var order = new OrdersAddDTO();
            order.AppUserId = appuserId;

            order.RestaurantId = orderItems[0].Items.RestaurantId;
            order.OrderNumber = await context.Orders.CountAsync() + 1;
            order.Totalprice = orderItems.Sum(x => x.TotalWithAmount);
            order.TotalVat = order.Totalprice * decimal.Parse("0.15");

            order.TotalAftetrVat = order.Totalprice + order.TotalVat;
            if (offer != null)
            {
                if (offer.OfferType == Domain.Entities.OfferType.Amount)
                {
                    if (order.Totalprice >= offer.MustExceed)
                    {
                        order.OffersId = offer.Id;
                        order.TotalDiscount = offer.Amount;
                        order.TotalAfterDiscount = order.TotalAftetrVat - offer.Amount;
                        order.FinalPrice = order.TotalAfterDiscount;
                    }
                }
                else if (offer.OfferType == Domain.Entities.OfferType.Percent)
                {
                    if (order.Totalprice >= offer.MustExceed)
                    {
                        order.OffersId = offer.Id;
                        order.TotalDiscount = order.TotalAftetrVat * offer.Percent;
                        order.TotalAfterDiscount = order.TotalAftetrVat - order.TotalDiscount;
                        order.FinalPrice = order.TotalAfterDiscount;
                    }
                }
            }
            else
            {
                order.TotalDiscount = 0;

                order.FinalPrice = order.Totalprice;
                order.FinalPrice = order.TotalAftetrVat;

            }

            var res = mapper.Map<OrdersResDTO>(context.Orders.Add(mapper.Map<Orders>(order)).Entity);
            context.SaveChanges();

            for (int i = 0; i < orderItems.Count; i++)
            {
                orderItems[i].OrdersId = res.Id;
            }
            context.SaveChanges();


            return Ok(res);
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
        [Route("UpdatePaidStatus")]
        public async Task<OrdersResDTO> UpdatePaidStatus(Guid Id)
        {
            var setting = await context.Orders
                          .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            setting.IsPaided = true;
            await context.SaveChangesAsync();
            return mapper.Map<OrdersResDTO>(setting);
        }



        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<ComplainResDTO> UpdateStatus(Guid Id, int Value, string Name)
        {
            var setting = await context.Orders
                         .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            var option = await context.OptionSetItems
            .Where(x => x.OptionSet.Name == Name && x.Value == Value).FirstOrDefaultAsync();
            setting.OrderStatusId = option.OptionSetId;
            await context.SaveChangesAsync();
            return mapper.Map<ComplainResDTO>(setting);
        }


        //[HttpPost]
        //[Route("Delete")]
        //public async Task<bool> Delete(Guid Id)
        //{

        //    var productInDb = await context.Orders
        //                    .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

        //    productInDb.IsDeleted = true;

        //    return await context.SaveChangesAsync() > 0;


        //}

        [HttpGet]
        [Route("GetAllByRestaurantId")]
        public async Task<List<OrdersResDTO>> GetAllByRestaurantId(Guid RestaurantId)
        {
            var res = await context.Orders.
                Where(x => x.IsDeleted == false && x.RestaurantId == RestaurantId).
                Include(x => x.OrderItems).ThenInclude(x => x.Items).ThenInclude(x => x.Photo).
                Include(x => x.OrderStatus).
                Include(x => x.Restaurant).
                ToListAsync();
            return mapper.Map<List<OrdersResDTO>>(res);
        }


        [HttpGet]
        [Route("GetAllByUserId")]
        public async Task<List<OrdersResDTO>> GetAllByUserId(string UserId)
        {
            var res = await context.Orders.
                Where(x => x.IsDeleted == false && x.AppUserId == UserId).
                Include(x => x.OrderItems).ThenInclude(x => x.Items).ThenInclude(x => x.Photo).
                Include(x => x.OrderStatus).
                Include(x => x.Restaurant).
                ToListAsync();
            return mapper.Map<List<OrdersResDTO>>(res);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<OrdersResDTO> GetById(Guid Id)
        {
            var res = await context.Orders.
                Where(x => x.IsDeleted == false && x.Id == Id).
                Include(x => x.OrderItems).ThenInclude(x => x.Items).ThenInclude(x => x.Photo).
                Include(x => x.OrderStatus).
                Include(x => x.Restaurant).
                ToListAsync();
            return mapper.Map<OrdersResDTO>(res);
        }


    }
}
