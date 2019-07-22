using PDV.Domain.Entities.Responses;
using System;

namespace PDV.Domain.Interfaces.Services
{
    public interface ITransacaoService : IDisposable
    {
        PagamentoResponse MostrarMensagemTrocoInteligente(decimal totalVenda, decimal valorPago);
    }
}
