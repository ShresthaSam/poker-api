using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokerApi.App_Start;
using PokerApi.Mappers;
using PokerApi.Models.Validators;
using PokerApi.Repositories;
using PokerApi.Services;
using Serilog;
using System.Reflection;

namespace PokerApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _assemblyName;
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            _logger = Log.Logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Debug("Startied configuring services");
            services.AddMvc();

            _logger.Debug("Configuring Swagger");
            SwaggerConfig.ConfigureSwagger(services, _assemblyName);

            _logger.Debug("Configuring Fluent Validation");
            services.AddControllers()
                .AddFluentValidation(fv => 
                {
                    fv.RegisterValidatorsFromAssemblyContaining<PokerHandDtoValidator>();
                    fv.ImplicitlyValidateChildProperties = true;
                });

            _logger.Debug("Finished configuring services");
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServicesAutofacConfigModule());
            builder.RegisterModule(new RepositoriesAutofacConfigModule());
            builder.RegisterModule(new MappersAutofacConfigModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _logger.Debug("Starting configuring HTTP request pipeline");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var swaggerEndpointUrl = Constants.OPEN_API_SWAGGER_JSON_URL;
                var swaggerEndpointName = _assemblyName;
                options.SwaggerEndpoint(swaggerEndpointUrl, swaggerEndpointName);

                options.DisplayOperationId();

                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            _logger.Debug("Finished configuring HTTP request pipeline");
        }
    }
}
