using PDV.Domain.Entities;
using PDV.Domain.Entities.Responses;
using PDV.Domain.Interfaces.Repositories;
using PDV.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDV.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITrasacaoRepository _transacaoRepository;

        public TransacaoService(ITrasacaoRepository transacaoRepository)
        {
            this._transacaoRepository = transacaoRepository;
        }

        public PagamentoResponse MostrarMensagemTrocoInteligente(decimal totalVenda, decimal valorPago)
        {
            var especiesPossiveis = new List<decimal>() { 0.01M, 0.05M, 0.10M, 0.50M, 10M, 20M, 50M, 100M };
            var especiesTroco = new List<decimal>();
            var transacao = new Transacao(totalVenda, valorPago);

            if (!PermitidaTransacao(totalVenda, valorPago))
                return MontarResposta("O valor pago é menor que o total da venda.", false);

            var troco = transacao.Troco;

            especiesPossiveis.OrderByDescending(ep => ep).ToList().ForEach((e) =>
            {
                var acumuloEspecie = (int)(troco/e);

                if (!acumuloEspecie.Equals(0))
                {
                    for (int i = 0; i < acumuloEspecie; i++)
                        especiesTroco.Add(e);
                }

                troco -= (acumuloEspecie * e);
            });

            if (!CorretoAcumuloEspecies(especiesTroco, transacao.Troco))
                return MontarResposta("Não foi possível contabilizar as especies para dar o troco.", false);

            var mensagem = $"Seu troco é de {string.Format("{0:C}", transacao.Troco)} e será entregue através das espécies: {Environment.NewLine}";

            var especieAgrupado = especiesTroco.GroupBy(e => e).Select(g => new { Especie = g.Key, Quantidade = g.Count() }).OrderByDescending(e => e.Especie);

            foreach (var especiesAgrupadoTroco in especieAgrupado)
                mensagem += $"- {especiesAgrupadoTroco.Quantidade.ToString()} especie(s) de {string.Format("{0:C}", especiesAgrupadoTroco.Especie)}{Environment.NewLine}";

            transacao.MensagemRetornada = mensagem;

            _transacaoRepository.Salvar(transacao);

            if (transacao.Id <= 0)
                return MontarResposta("Não foi possível gravar a transação no banco.", false);

            return MontarResposta(mensagem, true);
        }

        private bool PermitidaTransacao(decimal totalVenda, decimal valorPago)
        {
            return (totalVenda < valorPago);
        }

        private bool CorretoAcumuloEspecies(List<decimal> especies, decimal troco)
        {
            return (especies.Sum() == troco);
        }

        private PagamentoResponse MontarResposta(string mensagem, bool valido)
        {
            return new PagamentoResponse() { Mensagem = mensagem, Valido = valido };
        }

        public void Dispose()
        {
            
        }
    }
}
