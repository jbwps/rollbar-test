using ClassLibrary1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rollbar;
using Rollbar.NetCore.AspNet;

namespace WebApplication1
{
    public class Startup
    {
        private const string RollbarAccessToken = "Your Token Here";
        private const string RollbarEnvironment = "Development";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            RollbarLocator.RollbarInstance
               .Configure(new RollbarConfig(RollbarAccessToken)
               {
                   Environment = RollbarEnvironment,
                   Enabled = true
               });

            services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter = (loggerName, loglevel) => loglevel >= LogLevel.Error;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ITestService, TestService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRollbarMiddleware();

            app.UseMvc();
        }
    }
}
