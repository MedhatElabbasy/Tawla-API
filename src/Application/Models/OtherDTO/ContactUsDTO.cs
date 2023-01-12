using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings.Other;

namespace Tawala.Application.Models.OtherDTO
{
    public class ContactUsAddDTO : IMapFrom<ContactUs>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumner { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid? StatusId { get; set; }
    }
    public class ContactUsUpdateDTO : IMapFrom<ContactUs>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumner { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid? StatusId { get; set; }
    }
    public class ContactUsResDTO : IMapFrom<ContactUs>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumner { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid? StatusId { get; set; }
        public OptionSetItemResDTO Status { get; set; }
    }
}
