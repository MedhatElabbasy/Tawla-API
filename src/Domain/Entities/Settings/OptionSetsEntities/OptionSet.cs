using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;

namespace Tawala.Domain.Entities.Settings.OptionSetsEntities
{
    public class OptionSet : IEntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string DisplayNameAr { get; set; }
        public string DisplayNameEN { get; set; }
        public virtual IList<OptionSetItem> OptionSetItems { get; set; } = new List<OptionSetItem>();
    }
}
