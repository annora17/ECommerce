using EC.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Infrastructure.EFCore.Configurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<ECUser>
    {
        public void Configure(EntityTypeBuilder<ECUser> builder)
        {
            builder.ToTable("IdentityUser");

            builder.Property(q => q.Id).UseIdentityColumn(seed: 1000000);
        }
    }
}
