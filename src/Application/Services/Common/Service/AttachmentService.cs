using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Attachments;
using Tawala.Application.Models.Common;
using Tawala.Application.Services.Common.IService;
using Tawala.Domain.Entities.Settings;
using Tawala.Infrastructure;
using Tawala.Infrastructure.Services;
using Tawala.Infrastructure.Repository;

namespace Takid.Application.Services.Common.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly MyConfigService myConfigService;
        private readonly IRepositoryBase<AppAttachment> repository;
        private readonly IFileConversion fileConversion;
        private readonly IConfiguration configuration;

        public AttachmentService(IRepositoryBase<AppAttachment> _repository,
            IFileConversion _fileConversion,
            MyConfigService _myConfigService,
            IConfiguration _configuration)
        {
            this.repository = _repository;
            this.fileConversion = _fileConversion;
            this.myConfigService = _myConfigService;
            this.configuration = _configuration;
        }
        public async Task<AppAttachment> Add(AppAttachment attachment)
        {
            var result = await repository.Add(attachment);
            return result;
        }
    }
}
