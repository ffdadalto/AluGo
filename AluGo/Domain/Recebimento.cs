namespace AluGo.Domain
{
    public class Recebimento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ParcelaId { get; set; }
        public Parcela Parcela { get; set; } = default!;


        public DateTime DataPagamento { get; set; } = DateTime.UtcNow;
        public decimal ValorPago { get; set; }
        public string Observacao { get; set; }
        public string MeioPagamento { get; set; } = "PIX";

        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
