namespace AluGo.Domain
{
    public class Contrato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ImovelId { get; set; }
        public Imovel Imovel { get; set; } = default!;
        public Guid LocatarioId { get; set; }
        public Locatario Locatario { get; set; } = default!;


        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public byte DiaVencimento { get; set; } = 5;
        public decimal ValorAluguel { get; set; }
        public decimal? DescontoAteVencimento { get; set; }
        public decimal MultaPercentual { get; set; } = 2.0m; // %
        public decimal JurosAoDiaPercentual { get; set; } = 0.0333m; // % ao dia
        public string ReajusteIndice { get; set; } = "IPCA";
        public byte ReajustePeriodicidadeMeses { get; set; } = 12;
        public DateTime? ReajusteUltimaData { get; set; }
        public string Observacoes { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;


        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
        public ICollection<Reajuste> Reajustes { get; set; } = new List<Reajuste>();
    }
}
