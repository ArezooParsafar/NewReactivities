using Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.IocConfig
{
    public static class CustomIdentity
    {
        public static void AddCustomIdentityServices(this IServiceCollection services)
        {
            var siteSettings = services.GetSiteSettings();
            services.AddCustomServices();
            services.AddIdentityServices(siteSettings);
            services.AddCustomJwtBearer(siteSettings);
            services.AddConfiguredDbContext(siteSettings);
            services.AddApplicationServices();
            services.AddCustomCors();

        }
    }
}
