using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Domain.Entities.Category
{
    public class Category : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? MainCategoryId { get; set; }
        public Category MainCategory { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? CategoryPhotoId { get; set; }
        public AppAttachment CategoryPhoto { get; set; }
        
    }
}
