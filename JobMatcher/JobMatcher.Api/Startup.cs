using JobAdder.Infrastructure.ApiClients;
using JobMatcher.Application.Interfaces.ApiClients;
using JobMatcher.Application.Interfaces.Services;
using JobMatcher.Application.Services.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace JobMatcher.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<IApiClient, ApiClient>();
            services.AddTransient<ICandidateClient, CandidateClient>();
            services.AddTransient<IJobClient, JobClient>();
            services.AddTransient<IJobService, JobService>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
            );

            app.UseMvc();
        }
    }
}
