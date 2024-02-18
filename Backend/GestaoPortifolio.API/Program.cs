using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Infra.Context;
using GestaoPortfolio.Infra.Extensions;
using GestaoPortfolio.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortifolio.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "dev";

            IConfiguration configuration = builder.Configuration
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{env}.json", false)
                .Build();
                        
            // Add services to the container.
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.RegisterDb(configuration);
            builder.Services.RegisterFacades();
            builder.Services.RegisterServices();
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (env == "dev")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}