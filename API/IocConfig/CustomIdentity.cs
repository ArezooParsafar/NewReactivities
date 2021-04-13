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
            services.AddIdentityOptions(siteSettings);
            services.AddConfiguredDbContext(siteSettings);
            services.AddCustomServices();
            services.AddCustomJwtBearer();
            services.AddApplicationServices();
            services.AddCustomCors();
            
        }
    }
}
