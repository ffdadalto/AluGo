using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VContratoLista
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ImovelId { get; set; }
        public string ImovelNome { get; set; }
        public Guid LocatarioId { get; set; }
        public string LocatarioNome { get; set; }
        public int Numero { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorAluguel { get; set; }
        public decimal ValorContrato { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
