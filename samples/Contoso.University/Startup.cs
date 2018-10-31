using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using NSwag.AspNetCore;
using System.Reflection;
using Zek.Api;
using Zek.Api.Filters;
using NJsonSchema;

namespace Contoso.University
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Assemblies
            var asm = Assembly.GetExecutingAssembly();
            var zekApiAsm = typeof(BaseController).Assembly;

            // Logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            // MVC
            services.AddMvc(options =>
            {
                // Validation & Exception filters
                options.Filters.Add(typeof(ValidatorActionFilter));
                options.Filters.Add(typeof(HandleErrorFilterAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
            .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(asm));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Security
            // TODO: Configure keys
            services.AddDataProtection();

            services.AddSwagger();

            // Mediator
            services.AddMediatR(asm);

            // Auto Mapper
            services.AddAutoMapper(asm, zekApiAsm);

            Mapper.AssertConfigurationIsValid();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseCors(_ => {
                _.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin()
                 .AllowCredentials();
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
        }
    }
}
