
namespace PDV.Domain.Entities.Requests
{
    public class PagamentoRequest
    {
        //[DataMember(Name ="totalVenda")]
        public decimal TotalVenda { get; set; }

        //[DataMember(Name = "valorPago")]
        public decimal ValorPago { get; set; }
    }
}
