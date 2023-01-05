using Tawala.Domain.Common;
using Tawala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;

namespace Tawala.Infrastructure
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {

        private readonly ApplicationDbContext context;
        public EFCoreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRang(List<TEntity> entity)
        {
            context.Set<TEntity>().AddRange(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            entity.IsDeleted = true;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
            //context.Set<TEntity>().Remove(entity);
            //await context.SaveChangesAsync();
            //return entity;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(x => x.IsDeleted == false).Where(predicate);
            }

            return query;
        }

        public async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().Where(x => x.IsDeleted == false && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Detached;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<int> ExecuteSql(string sql, params object[] parameters)
        {
            return await context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}