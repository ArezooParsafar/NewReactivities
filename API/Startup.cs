using API.IocConfig;
using Domain.Identity;
using Domain.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Persistence.Context;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt =>
                       {
                           var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                           opt.Filters.Add(new AuthorizeFilter(policy));
                       });
            services.AddOptions<BearerTokensOptions>().Bind(Configuration.GetSection("BearerTokensOptions"));
            services.Configure<SiteSettings>(options => Configuration.Bind(options));
            services.AddCustomIdentityServices();
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error/index/500");
            app.UseStatusCodePagesWithReExecute("/error/index/{0}");
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseRouting();
            // Note: it has to be before Authorization
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
