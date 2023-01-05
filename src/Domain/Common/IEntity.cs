using System;

namespace Tawala.Domain.Common
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }

  
}
