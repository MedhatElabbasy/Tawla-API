using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.SettingDTO.OptionSetDTOS;
using Tawala.Domain.Entities.Settings.EvaluationEN;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Application.Models.SettingDTO
{
    public class EvaluationAddDTO : IMapFrom<Evaluation>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public int ScoreNumber { get; set; }
        public Guid? EvaluationTypeId { get; set; }
    }
    public class EvaluationUpdateDTO : IMapFrom<Evaluation>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public int ScoreNumber { get; set; }
        public Guid? EvaluationTypeId { get; set; }
    }
    public class EvaluationResDTO : IMapFrom<Evaluation>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public int ScoreNumber { get; set; }
        public Guid? EvaluationTypeId { get; set; }
        public OptionSetItemResDTO EvaluationType { get; set; }

    }
}
