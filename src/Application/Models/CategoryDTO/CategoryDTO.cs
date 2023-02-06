using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.Common;
using Tawala.Domain.Entities.CategoryEni;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.CategoryDTO
{
    public class CategoryAddDTO : IMapFrom<Category>
    {
        public Guid? MainCategoryId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? CategoryPhotoId { get; set; }
    }
    public class CategoryUpdateDTO : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public Guid? MainCategoryId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? CategoryPhotoId { get; set; }
    }
    public class CategoryResDTO : IMapFrom<Category>
    {
        public Guid Id { get; set; }

        public Guid? MainCategoryId { get; set; }
        public CategoryResDTO MainCategory { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public Guid? CategoryPhotoId { get; set; }
        public AppAttachmentResDTO CategoryPhoto { get; set; }
    }
}
