using GestaoPortfolio.Application.Services;
using GestaoPortfolio.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IIncluirProdutoService, IncluirProdutoService>();
            services.AddTransient<IAlterarProdutoService, AlterarProdutoService>();
            services.AddTransient<IExcluirProdutoService, ExcluirProdutoService>();
        }
    }
}
