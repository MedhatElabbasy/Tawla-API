using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Domain.Entities.Settings.Evaluation
{
    public class Evaluation : AuditableEntity, IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public int ScoreNumber { get; set; }
        public Guid? EvaluationTypeId { get; set; }
        public OptionSetItem EvaluationType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
