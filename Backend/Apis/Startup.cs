using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using Models.Entities;
using Services.Services;
using Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;
using Apis.Middlewares;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks; // Add this namespace
using HealthChecks.UI.Client;

namespace Apis
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
            services.AddControllers();

            services.AddHealthChecks().AddSqlServer(Configuration.GetConnectionString("Connection"), tags: new[] { "db" });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;

                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.ApiVersionReader = ApiVersionReader.Combine(

                    new QueryStringApiVersionReader("api-version"),

                    new HeaderApiVersionReader("X-API-Version")
                );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Task", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(

                options => options.UseSqlServer(

                    Configuration.GetConnectionString("Connection"),

                    x => x.MigrationsAssembly("Models")
                )
            );

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        ;
                });
            });

            services.AddSingleton(services => LoggerFactory.Create(builder =>
            {
                builder.AddNLog(Configuration);
            }));

            services.AddScoped<TaskToDoService>();

            services.AddScoped<IValidator<TaskToDo>, TaskToDoValidator>();

            services.AddScoped<JWTService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors("AllowAllOrigins");

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apis v1"));

            app.UseHealthChecks("/health");

            app.UseHealthChecks("/health/database", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("db"),

                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JWTMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
