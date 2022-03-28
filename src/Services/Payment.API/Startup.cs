using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Payment.API.Application.Commands.Authorize;
using Payment.API.Common.Behaviors;
using Payment.API.Common.ExceptionHandling;
using Payment.API.Common.Mappers;
using Payment.Infrastructure;
using Payment.Infrastructure.ServiceRegistrationExtensions;
using System.Reflection;

namespace Payment.API
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
            services.AddControllers();


            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payment API",
                    Description = "A simple example ASP.NET Core Web API"
                });
            });

            services.AddDbContext<TransactionDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    a => a.MigrationsAssembly(typeof(TransactionDbContext).Assembly.FullName));
            });
            services.RegisterInfrastructureServices();

            services.AddMediatR(typeof(AuthorizeCommandHandler).GetTypeInfo().Assembly);
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperGeneralMapping).GetTypeInfo().Assembly });

            services.AddMvcCore().AddApiExplorer();

            services.AddMvc();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<AuthorizeCommandValidator>();
            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
