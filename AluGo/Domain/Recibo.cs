namespace AluGo.Domain
{
    public class Recibo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ParcelaId { get; set; }
        public Parcela Parcela { get; set; } = default!;


        public int Numero { get; set; }
        public string CaminhoArquivo { get; set; } = string.Empty;
        public DateTime GeradoEm { get; set; } = DateTime.UtcNow;
    }
}
