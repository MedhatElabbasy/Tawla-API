using Tawala.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tawala.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity 
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddRang(List<TEntity> entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(int id);
        Task<int> ExecuteSql(string sql, params object[] parameters);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }


}
