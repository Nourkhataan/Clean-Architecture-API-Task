
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Products.Application;
using Products.Application.Interfaces;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Repositories;
using Serilog;

namespace Product.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddApplication();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionFeature =
                        context.Features.Get<IExceptionHandlerFeature>();

                    var exception = exceptionFeature?.Error;

                    var logger = context.RequestServices
                        .GetRequiredService<ILogger<Program>>();

                    if (exception is not null)
                    {
                        logger.LogError(
                            exception,
                            "Unhandled exception occurred while processing {Method} {Path}",
                            context.Request.Method,
                            context.Request.Path);
                    }

                    context.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "An unexpected error occurred."
                    });
                });
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
