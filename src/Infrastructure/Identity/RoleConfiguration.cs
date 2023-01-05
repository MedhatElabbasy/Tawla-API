using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Infrastructure.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppIdentityRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityRole> builder)
        {
            //builder.HasData(
            //   new AppIdentityRole
            //   {
            //       Name = "Admin",
            //       NameAR = "مدير",
            //       NormalizedName = "ADMIN"
            //   },
            //    new AppIdentityRole
            //    {
            //        Name = "User",
            //        NameAR = "مستخدم",
            //        NormalizedName = "User".ToUpper()
            //    });
        }
    }
}
