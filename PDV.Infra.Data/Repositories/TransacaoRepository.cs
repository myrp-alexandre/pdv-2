using PDV.Domain.Entities;
using PDV.Domain.Interfaces.Repositories;
using PDV.Infra.Data.Context;

namespace PDV.Infra.Data.Repositories
{
    public class TransacaoRepository : ITrasacaoRepository
    {
        private readonly PdvContext _contexto;

        public TransacaoRepository(PdvContext contexto)
        {
            this._contexto = contexto;
        }

        public Transacao Salvar(Transacao transacao)
        {
            _contexto.Transacoes.Add(transacao);
            _contexto.SaveChanges();
            return transacao;
        }
    }
}
