using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Infrastructure.Persistence;

namespace Tawala.Infrastructure.Repository
{
    public class EFRepositoryBase<TEntity> : IRepositoryBase<TEntity>
      where TEntity : class, IEntityBase
    {

        private readonly ApplicationDbContext context;
        public EFRepositoryBase(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            if (entity.Id == new Guid())
            {
                entity.Id = Guid.NewGuid();
            }
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

        public async Task<TEntity> Delete(string id)
        {
            var entity = await context.Set<TEntity>().FindAsync(new Guid(id));
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

        public async Task<TEntity> Get(string id)
        {
            return await context.Set<TEntity>().Where(x => x.IsDeleted == false && x.Id == new Guid(id)).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<TEntity> Update(TEntity entity)
        {
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
