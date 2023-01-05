using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Infrastructure.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IEntityBase
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(string id);
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddRang(List<TEntity> entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(string id);
        Task<int> ExecuteSql(string sql, params object[] parameters);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
