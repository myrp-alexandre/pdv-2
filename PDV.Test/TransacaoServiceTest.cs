using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDV.Domain.Interfaces.Repositories;
using Moq;
using PDV.Domain.Services;

namespace PDV.Test
{
    [TestClass]
    public class TransacaoServiceTest
    {
        [TestMethod]
        public void FazerTransacaoValorPagoInferiorTotalVendaFalha()
        {
            var mockTransacao = new Mock<ITrasacaoRepository>();
            
            decimal totalVenda = 105.42M;
            decimal valorPago = 90.10M;

            var transacaoService = new TransacaoService(mockTransacao.Object);
            var transacaoResponse = transacaoService.MostrarMensagemTrocoInteligente(totalVenda, valorPago);

            Assert.IsFalse(transacaoResponse.Valido);
        }

        [TestMethod]
        public void FazerTransacaoPagarAcimaIgualTotalVendaSucesso()
        {
            var mockTransacao = new Mock<ITrasacaoRepository>();

            decimal totalVenda = 101.01M;
            decimal valorPago = 150.00M;

            var transacaoService = new TransacaoService(mockTransacao.Object);
            var transacaoResponse = transacaoService.MostrarMensagemTrocoInteligente(totalVenda, valorPago);

            Assert.IsTrue(transacaoResponse.Valido);
        }
    }
}
