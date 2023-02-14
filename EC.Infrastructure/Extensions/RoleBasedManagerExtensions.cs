using EC.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Infrastructure.Extensions
{
    public static class RoleBasedManagerExtensions
    {
        public static async void AddRolesAsync(this IServiceCollection services)
        {
            using (var provider = services.BuildServiceProvider())
            {
                var roleBasedManager = (RoleBasedManager)provider.GetService<RoleBasedManager>();

                if(roleBasedManager != null)
                {
                    await roleBasedManager.CreateRoleAsync();
                }
            }
        }
    }
}
