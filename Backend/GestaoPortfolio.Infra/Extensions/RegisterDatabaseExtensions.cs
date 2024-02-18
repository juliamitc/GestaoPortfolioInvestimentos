using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Infra.Context;
using GestaoPortfolio.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterDatabaseExtensions
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("Database:ConnectionString");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }
    }
}
