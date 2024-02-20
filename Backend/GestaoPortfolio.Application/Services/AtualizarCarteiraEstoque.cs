using GestaoPortfolio.Application.Facades;
using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;

namespace GestaoPortfolio.Application.Services
{
    public class AtualizarCarteiraEstoqueService : IAtualizarCarteiraEstoqueService
    {
        private readonly IOfertaFacade ofertaFacade;
        private readonly IOperacaoFacade operacaoFacade;
        private readonly ICarteiraFacade carteiraFacade;
        private readonly IClienteRepository clienteRepository;
        private readonly IOfertaRepository ofertaRepository;
        private readonly ICarteiraRepository carteiraRepository;


        public AtualizarCarteiraEstoqueService(IOperacaoFacade operacaoFacade, IOfertaFacade ofertaFacade, ICarteiraFacade carteiraFacade, ICarteiraRepository carteiraRepository, IOfertaRepository ofertaRepository, IClienteRepository clienteRepository)
        {
            this.ofertaFacade = ofertaFacade;
            this.carteiraFacade = carteiraFacade;
            this.operacaoFacade = operacaoFacade;
            this.clienteRepository = clienteRepository;
            this.carteiraRepository = carteiraRepository;  
            this.ofertaRepository = ofertaRepository;
        }

        public Task<Operacao> TratarOperacao(Operacao operacao)
        {
            Oferta oferta = (Oferta)ofertaRepository.GetById(operacao.CodigoOferta);
            Cliente cliente = (Cliente)clienteRepository.GetById(operacao.IdCliente);
            operacao.ValorPrecoUnitario = oferta.PrecoUnitario;
            operacao.ValorTotalOperacao = (operacao.ValorPrecoUnitario * operacao.QuantidadeOperacao);
            operacao.QuantidadeDisponivelEstoque = oferta.QuantidadeDisponivel;

            var resultado = operacaoFacade.Inserir(operacao);

            if (operacao.Evento == Evento.Compra)
            {
                AtualizarCarteiraEstoqueCompra(oferta, operacao, cliente);
            }
            else
            {
                Carteira carteira = carteiraRepository.BuscarCarteiraCliente(operacao.CodigoOferta, operacao.IdCliente);

                AtualizarCarteiraEstoqueVenda(oferta, carteira, operacao);
            }

            return resultado;
        }
        public void AtualizarCarteiraEstoqueVenda(Oferta oferta, Carteira carteira, Operacao operacao)
        {
            oferta.QuantidadeDisponivel += operacao.QuantidadeOperacao;
            ofertaFacade.Alterar(oferta);

            if(carteira.Quantidade == operacao.QuantidadeOperacao)
            {
                carteiraFacade.ExcluirPosicao(carteira.Id);
            } else
            {
                carteira.Quantidade -= operacao.QuantidadeOperacao;
                carteiraFacade.AlterarPosicao(carteira);
            }
        }

        public void AtualizarCarteiraEstoqueCompra(Oferta oferta, Operacao operacao, Cliente cliente)
        {
            Carteira carteira = new Carteira();

            oferta.QuantidadeDisponivel -= operacao.QuantidadeOperacao;
            ofertaFacade.Alterar(oferta);

            carteira.IdCliente = cliente.Id;
            carteira.NomeCliente = cliente.Nome;
            carteira.IdOperacao = operacao.IdOperacao;
            carteira.CodigoOferta = oferta.CodigoOferta;
            carteira.Papel = oferta.Papel;
            carteira.Quantidade = operacao.QuantidadeOperacao;
            carteira.PrecoUnitario = oferta.PrecoUnitario;
            carteira.ValorTotalOperacao = operacao.ValorTotalOperacao;
            carteira.DataVencimento = oferta.DataVencimento;
            carteiraFacade.IncluirPosicao(carteira);

        }
    }
}
