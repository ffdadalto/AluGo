namespace AluGo.Domain
{
    public class Reajuste
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ContratoId { get; set; }
        public Contrato Contrato { get; set; } = default!;


        public DateTime DataBase { get; set; }
        public decimal Percentual { get; set; }
        public string Observacao { get; set; }
        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
