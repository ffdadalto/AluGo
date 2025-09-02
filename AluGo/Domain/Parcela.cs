namespace AluGo.Domain
{
    public class Parcela
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ContratoId { get; set; }
        public Contrato Contrato { get; set; } = default!;
        public int Numero { get; set; }

        public string Competencia { get; set; } = string.Empty; // AAAA-MM
        public DateTime DataVencimento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorMulta { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorOutros { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusParcela Status { get; set; } = StatusParcela.spAberta;
        public DateTime? QuitadaEm { get; set; }
        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;


        public ICollection<Recebimento> Recebimentos { get; set; } = new List<Recebimento>();
        public Recibo Recibo { get; set; }
    }
}
