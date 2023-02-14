using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using EC.Infrastructure.EFCore.DbContexts;

namespace EC.Infrastructure.EFCore.Extensions
{
    public static class ConfigurationExtension
    {
        public static List<object> ApplyAllConfiguration(this ModelBuilder modelBuilder)
        {
            var applyConfigurationMethodInfo = modelBuilder.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .First(m => m.Name.Equals("ApplyConfiguration",StringComparison.OrdinalIgnoreCase));

            var ret = typeof(ECContext).Assembly.GetTypes()
                .Select(t => (t, i: t.GetInterfaces().FirstOrDefault(it => it.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.OrdinalIgnoreCase))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj })).ToList();

            return ret;
        }
    }
}
