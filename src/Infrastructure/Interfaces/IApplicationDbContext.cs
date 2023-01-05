using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Infrastructure.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; } 

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
