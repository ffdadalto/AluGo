using System.Diagnostics.Contracts;

namespace AluGo.Domain
{
    public class Imovel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Apelido { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = "";
        public TipoImovel Tipo { get; set; } = TipoImovel.tiApartamento;
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;


        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
