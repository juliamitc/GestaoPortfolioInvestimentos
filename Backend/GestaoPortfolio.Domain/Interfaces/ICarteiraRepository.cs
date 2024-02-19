using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface ICarteiraRepository : IBaseRepository<Carteira>
    {
        Task<IEnumerable<Carteira>> Listar(Carteira carteira);
    }
}
