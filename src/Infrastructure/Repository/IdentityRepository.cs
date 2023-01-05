using Tawala.Domain.Common;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Infrastructure
{
    public class IdentityRepository : IIdentityRepository
    {

        private readonly ApplicationDbContext context;
        public IdentityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppUser> Update(AppUser entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
        public  IQueryable<AppUser> Find(Expression<Func<AppUser, bool>> predicate, params Expression<Func<AppUser, object>>[] includeProperties)
        {
            IQueryable<AppUser> query = context.Set<AppUser>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
           
            return query;
        }

        public async Task<AppUser> Get(string id)
        {
            return await context.Set<AppUser>().FindAsync(id);
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await context.Set<AppUser>().ToListAsync();
        } 
        

         
    }
}