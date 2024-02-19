using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Kafka
{
    public class OrdemCompraConsumidor : ConsumidorKafka, IOrdemCompraConsumidor
    {
        public OrdemCompraConsumidor(IConfiguration configuration) 
            : base(configuration)
        {
        }

        protected override async Task Processar(string mensagem)
        {
            Debug.WriteLine(mensagem);
        }
    }
}
