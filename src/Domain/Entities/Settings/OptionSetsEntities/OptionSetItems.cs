using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Settings.OptionSetsEntities
{
    public class OptionSetItem : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public int Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid? OptionSetId { get; set; }
        public OptionSet OptionSet { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
    }
}
