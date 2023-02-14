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
    public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<ECUserClaim>
    {
        public void Configure(EntityTypeBuilder<ECUserClaim> builder)
        {
            builder.ToTable("IdentityUserClaim");
        }
    }
}
