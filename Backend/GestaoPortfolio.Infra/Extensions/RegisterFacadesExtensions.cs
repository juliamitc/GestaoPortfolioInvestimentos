using GestaoPortfolio.Application.Facades;
using GestaoPortfolio.Domain.Interfaces.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterFacadesExtensions
    {
        public static void RegisterFacades(this IServiceCollection services)
        {
            services.AddTransient<IProdutoFacade, ProdutoFacade>();
        }
    }
}
