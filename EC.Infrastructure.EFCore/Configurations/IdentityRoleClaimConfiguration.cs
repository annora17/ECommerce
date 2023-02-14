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
    public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<ECRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ECRoleClaim> builder)
        {
            builder.ToTable("IdentityRoleClaim");
        }
    }
}
