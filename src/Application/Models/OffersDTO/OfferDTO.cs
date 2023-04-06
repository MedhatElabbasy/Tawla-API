using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Models.Common;
using Tawala.Domain.Entities;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.OffersDTO
{
    public class OffersAddDTO : IMapFrom<Offers>
    {
        public string Code { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public OfferType OfferType { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public decimal MustExceed { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid? PhotoId { get; set; }
    }

    public class OffersUpdateDTO : IMapFrom<Offers>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public OfferType OfferType { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public decimal MustExceed { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid? PhotoId { get; set; }
    }

    public class OffersResDTO : IMapFrom<Offers>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public OfferType OfferType { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public decimal MustExceed { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid? PhotoId { get; set; }
        public AppAttachmentResDTO Photo { get; set; }
    }

}
