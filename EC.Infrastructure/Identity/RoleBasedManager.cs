using EC.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Infrastructure.Identity
{
    public class RoleBasedManager
    {
        public const string AdminSchema = "Admin";
        public const string B2CUserSchema = "Customer";
        public const string B2BUserSchema = "CommercialUser";

        private readonly RoleManager<ECRole> _roleManager;


        public RoleBasedManager(RoleManager<ECRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync()
        {
            var adminRole = await _roleManager.FindByNameAsync(AdminSchema);

            if (adminRole == null)
                await _roleManager.CreateAsync(new ECRole { Name = AdminSchema });

            var b2cUserRole = await _roleManager.FindByNameAsync(B2CUserSchema);

            if (b2cUserRole == null)
                await _roleManager.CreateAsync(new ECRole { Name = B2CUserSchema });

            var b2bUserRole = await _roleManager.FindByNameAsync(B2BUserSchema);

            if (b2bUserRole == null)
                await _roleManager.CreateAsync(new ECRole { Name = B2BUserSchema });
        }
    }

    
}
