using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Domain.Common;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.SettingDTO
{
    public class SettingsAddDTO : IMapFrom<Settings>
    {

        public string Key { get; set; }
        public string KeyAr { get; set; }
        public string Value { get; set; }
        public bool IsLocked { get; set; }
    }

    public class SettingsUpdateDTO : IMapFrom<Settings>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string KeyAr { get; set; }
        public string Value { get; set; }
        public bool IsLocked { get; set; }
    }
    public class SettingsResDTO : IMapFrom<Settings>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string KeyAr { get; set; }
        public string Value { get; set; }
        public bool IsLocked { get; set; }
    }
}
