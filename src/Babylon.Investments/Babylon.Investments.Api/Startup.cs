using System;
using Babylon.Investments.Api.Logging;
using Babylon.Investments.Domain.Constants;
using Babylon.Investments.Injection.Injector;
using Babylon.Investments.Shared.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace Babylon.Investments.Api
{
    public class Startup
    {
        private static bool IsDevEnvironment
        {
            get
            {
                var useDevEnvironment = Configuration.GetValue<string>(EnvironmentVariables.UseDevelopmentEnvironment)
                                        ?? Environment.GetEnvironmentVariable(EnvironmentVariables.UseDevelopmentEnvironment);

                return StringComparer.InvariantCultureIgnoreCase.Equals(
                    useDevEnvironment,
                    bool.TrueString);
            }
        }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"Starting to configure services for Babylon Investments API");
            
            ServiceInjector.Configure(services, Configuration);
            
            services.AddControllers();

            services.AddLogging(logBuilder => logBuilder.AddDebug().AddSerilog(LoggerBuilder.Configure()));

            if (IsDevEnvironment)
            {
                services.AddSwaggerGen(gen =>
                {
                    gen.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Version = "v1",
                        Title = "Babylon Investments API",
                        Description = "Babylon Investments API to handle investment Investments and portfolios",
                    });
                });   
            }

            Console.WriteLine("Finished configuring services for Babylon Investments API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}
