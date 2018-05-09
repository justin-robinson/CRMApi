using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using CRMApi.AWS.DynamoDB;

namespace CRMApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }


            if (Configuration["AWS:DynamoDB:Client:ServiceURL"] != null) {
                Client.CreateClient(Configuration["AWS:DynamoDB:Client:ServiceURL"]);
            } else {
                Client.CreateClient(
                    Configuration["AWS:DynamoDB:Client:ProfileName"],
                    Configuration["AWS:DynamoDB:Client:RegionEndpointName"]
                );
            }         

            app.UseMvc();
        }
    }
}
