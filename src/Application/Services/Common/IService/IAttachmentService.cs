using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Models.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Services.Common.IService
{
    public interface IAttachmentService
    {
        Task<AppAttachment> Add(AppAttachment attachment);
    }
}
