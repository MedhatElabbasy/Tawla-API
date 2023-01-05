using Tawala.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Infrastructure
{
    public interface IIdentityRepository
    {
        Task<List<AppUser>> GetAll();
        Task<AppUser> Get(string id);
        Task<AppUser> Update(AppUser entity);
        IQueryable<AppUser> Find(Expression<Func<AppUser, bool>> predicate, params Expression<Func<AppUser, object>>[] includeProperties);
    }
}
