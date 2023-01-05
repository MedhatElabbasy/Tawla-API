using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Tawala.Domain.Entities.Settings.OptionSetsEntities;

namespace Tawala.Infrastructure.Persistence.Configurations
{
    //public class ClientOrderConfigurations : IEntityTypeConfiguration<ClientOrder>
    //{
    //    public void Configure(EntityTypeBuilder<ClientOrder> builder)
    //    {
    //        //  builder.HasMany(o => o.ContractType).WithOne().HasForeignKey(d => d.UserId).IsRequired();
    //        //builder.HasKey(a => a.Id);
    //        //builder.HasOne(x => x.ContractType).
    //        //                                    WithMany().
    //        //                                    HasForeignKey(x => x.ContractTypeId).
    //        //                                    OnDelete(DeleteBehavior.SetNull);
    //    }
    //}

}
