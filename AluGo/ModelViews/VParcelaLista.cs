using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VParcelaLista
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public Guid ContratoId { get; set; }
        public int ContratoNumero { get; set; }
        public string LocatarioNome { get; set; }
        public string ImovelNome { get; set; }
        public string Competencia { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusParcela Status { get; set; }
        public DateTime? QuitadaEm { get; set; }
        public decimal ValorTotalPago { get; set; }
        public decimal ValorAPagar => ValorTotal - ValorTotalPago;
        public bool Quitada { get; set; }
    }
}
