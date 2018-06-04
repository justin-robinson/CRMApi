using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Amazon.Lambda.Core;

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
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.WithOrigins("*"));
            }


            if (Configuration["AWS:DynamoDB:Client:ServiceURL"] != null) {
                LambdaLogger.Log($"Initializing DynamoDB client with service url: {Configuration["AWS:DynamoDB:Client:ServiceURL"]}");
                Client.CreateClient(Configuration["AWS:DynamoDB:Client:ServiceURL"]);
            } else if (Configuration["AWS:DynamoDB:Client:ProfileName"] != null && Configuration["AWS:DynamoDB:Client:RegionEndpointName"] != null) {
                LambdaLogger.Log($"Initializing DynamoDB client with profile: {Configuration["AWS:DynamoDB:Client:ProfileName"]}");
                Client.CreateClient(
                    Configuration["AWS:DynamoDB:Client:ProfileName"],
                    Configuration["AWS:DynamoDB:Client:RegionEndpointName"]
                );
            } else {
                LambdaLogger.Log("Initializing DynamoDB client with no args");
                Client.CreateClient();
            }

            app.UseMvc();
        }
    }
}
