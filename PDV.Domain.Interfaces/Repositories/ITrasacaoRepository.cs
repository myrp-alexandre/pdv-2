
using PDV.Domain.Entities;

namespace PDV.Domain.Interfaces.Repositories
{
    public interface ITrasacaoRepository
    {
        Transacao Salvar(Transacao transacao);
    }
}
