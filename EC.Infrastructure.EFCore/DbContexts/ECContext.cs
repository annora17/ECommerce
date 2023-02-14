using EC.Application.Interfaces;
using EC.Infrastructure.EFCore.Extensions;
using EC.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Infrastructure.EFCore.DbContexts
{
    public class ECContext : IdentityDbContext<ECUser, ECRole, int, ECUserClaim, ECUserRole, ECUserLogin, ECRoleClaim, ECUserToken>, IDatabaseContext
    {
        public ECContext(DbContextOptions<ECContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfiguration();
        }

    }
}
