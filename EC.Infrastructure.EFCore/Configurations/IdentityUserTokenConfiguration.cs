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
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<ECUserToken>
    {
        public void Configure(EntityTypeBuilder<ECUserToken> builder)
        {
            builder.ToTable("IdentityUserToken");
        }
    }
}
