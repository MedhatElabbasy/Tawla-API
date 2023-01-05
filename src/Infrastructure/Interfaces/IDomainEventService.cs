using Tawala.Domain.Common;
using System.Threading.Tasks;

namespace Tawala.Infrastructure.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
