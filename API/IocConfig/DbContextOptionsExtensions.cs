using Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.IocConfig
{
    public static class DbContextOptionsExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.DatabaseConfiguration(siteSettings);
            });

            return services;
        }
    }
}
