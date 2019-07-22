using System;

namespace PDV.Domain.Entities
{
    public class Transacao
    {
        public Transacao(decimal total, decimal valorPago)
        {
            this.ValorPago = valorPago;
            this.Total = total;
            this.Troco = this.ValorPago - this.Total;
            this.DataOperacao = DateTime.Now;
        }

        public int Id { get; private set; }

        public decimal ValorPago { get; private set; }

        public decimal Total { get; private set; }

        public decimal Troco { get; private set; }

        public DateTime DataOperacao { get; set; }

        public string MensagemRetornada { get; set; }

    }
}
