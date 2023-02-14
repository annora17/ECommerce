using EC.Application.Interfaces;
using EC.Application.Services;
using EC.Domain.Entities.Customers;
using EC.Infrastructure.EFCore.DbContexts;
using EC.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Infrastructure.EFCore
{
    public class StartupEFCore
    {
        private readonly IConfiguration Configuration;
        private readonly bool developmentMode;
        public StartupEFCore(IConfiguration configuration)
        {
            Configuration = configuration;
            bool.TryParse(Configuration.GetSection("DevelopmentMode").Value, out developmentMode);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (!developmentMode)
            {
                services.AddDbContext<ECContext>(o => o.UseSqlServer(Configuration.GetConnectionString("CommerceConnection")));
            }
            else
            {
                services.AddDbContext<ECContext>(o => o.UseSqlServer(Configuration.GetConnectionString("CommerceConnection")));
            }

            //It is the field where the repositories are added with DependencyInjection.
            #region AddRepositories
            services.AddScoped<IRepository<Customer, int>, EfCoreRepository<Customer, int>>();
            services.AddScoped<IAsyncRepository<Customer, int>, EfCoreRepository<Customer, int>>();

            #endregion

            //It is the field where the services are added with DependencyInjection.
            #region AddServices
            services.AddScoped<RoleBasedManager>();
            services.AddScoped<ICustomerService, CustomerService>();
            #endregion
        }
    }
}
