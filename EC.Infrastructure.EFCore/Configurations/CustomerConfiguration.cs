using EC.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Infrastructure.EFCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();

            //builder.HasMany(e => e.CustomerAddresses).WithOne(adr => adr.Customer);

            builder.OwnsMany(q => q.CustomerAddresses, opt =>
            {
                opt.ToTable("CustomerAddress");
                opt.Property(c => c.AddressName).HasMaxLength(32);
                opt.Property(c => c.Country).IsRequired().HasMaxLength(32);
                opt.Property(c => c.City).IsRequired().HasMaxLength(32);
                opt.Property(c => c.State).IsRequired().HasMaxLength(32);
                opt.Property(c => c.Quarter).IsRequired().HasMaxLength(32);
                opt.Property(c => c.CSBM).IsRequired().HasMaxLength(64);
                opt.Property(c => c.OutDoorNumber).IsRequired().HasMaxLength(8);
                opt.Property(c => c.InDoorNumber).IsRequired().HasMaxLength(8);
                opt.Property(c => c.PostalCode).HasMaxLength(16);
                opt.Property(c => c.IsDefault).HasDefaultValue(false);
                opt.Property(c => c.FullAddress).HasMaxLength(128);
            });

            builder.OwnsMany(q => q.CustomerBillingAddresses, opt =>
            {
                opt.ToTable("CustomerBillingAddress");
                opt.Property(c => c.AddressName).HasMaxLength(32);
                opt.Property(c => c.Country).IsRequired().HasMaxLength(32);
                opt.Property(c => c.City).IsRequired().HasMaxLength(32);
                opt.Property(c => c.State).IsRequired().HasMaxLength(32);
                opt.Property(c => c.Quarter).IsRequired().HasMaxLength(32);
                opt.Property(c => c.CSBM).IsRequired().HasMaxLength(64);
                opt.Property(c => c.OutDoorNumber).IsRequired().HasMaxLength(8);
                opt.Property(c => c.InDoorNumber).IsRequired().HasMaxLength(8);
                opt.Property(c => c.PostalCode).HasMaxLength(16);
                opt.Property(c => c.IsDefault).HasDefaultValue(false);
                opt.Property(c => c.FullAddress).HasMaxLength(128);
            });

            builder.Metadata.FindNavigation(nameof(Customer.CustomerAddresses)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Customer.CustomerBillingAddresses)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
