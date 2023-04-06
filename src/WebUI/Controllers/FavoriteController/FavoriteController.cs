using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Models.FavoriteDTO;
using Tawala.Infrastructure.Persistence;
using Tawala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tawala.WebUI.Controllers.FavoriteController
{
    public class FavoriteController : ApiControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEncryptDecryptService encryptDecryptService;
        private readonly IMapper mapper;
        public FavoriteController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public FavoriteResDTO Create(FavoriteAddDTO model)
        {
            var res = mapper.Map<FavoriteResDTO>(context.Favorite.Add(mapper.Map<Favorite>(model)).Entity);
            context.SaveChanges();
            return res;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(Guid Id)
        {

            var productInDb = await context.Favorite
                            .SingleOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            productInDb.IsDeleted = true;

            return await context.SaveChangesAsync() > 0; 
        }
        [HttpGet]
        [Route("GetAllByUserId")]
        public async Task<List<FavoriteResDTO>> GetAllByUserId(string userId)
        {
            var res = await context.Favorite.Where(x => x.IsDeleted == false && x.AppUserId == userId)
                  .Include(x => x.Restaurant).ThenInclude(x => x.Logo)
                .ToListAsync();
            return mapper.Map<List<FavoriteResDTO>>(res);
        }
    }
}
