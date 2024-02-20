using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GestaoPortfolio.Application.Kafka
{
    public class OrdemCompraConsumidor : ConsumidorKafka, IOrdemCompraConsumidor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public OrdemCompraConsumidor(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory) 
            : base(configuration)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task Processar(string mensagem)
        {
            Debug.WriteLine(mensagem);

            Oferta oferta = JsonConvert.DeserializeObject<Oferta>(mensagem);

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var ofertaFacade = scope.ServiceProvider.GetRequiredService<IOfertaFacade>();

                 await ofertaFacade.Inserir(oferta);
            }
        }
    }
}
