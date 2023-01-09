using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Application.Models.SettingDTO.OptionSetDTOS
{
    public class OptionSetItemAddDTO : IMapFrom<OptionSetItem>
    {
        public int Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid? OptionSetId { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
    }
    public class OptionSetItemUpdateDTO : IMapFrom<OptionSetItem>
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid? OptionSetId { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
    }
    public class OptionSetItemResDTO : IMapFrom<OptionSetItem>
    {

        public Guid Id { get; set; }
        public int Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid? OptionSetId { get; set; }
        public OptionSetResDTO OptionSet { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
    }
}
