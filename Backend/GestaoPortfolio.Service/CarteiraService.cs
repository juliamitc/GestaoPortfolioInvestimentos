using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Service
{
    public class CarteiraService
    {
        private readonly IBaseRepository<Carteira> _carteiraRepository;
        public CarteiraService(IBaseRepository<Carteira> carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }
        public Carteira GetCarteiraCliente(int idCliente)
        {
            Carteira carteira = _carteiraRepository.GetById(idCliente);
            return carteira;
        }
    }
}