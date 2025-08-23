using System.Diagnostics.Contracts;

namespace AluGo.Domain
{
    public class Locatario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty; // CPF/CNPJ
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;


        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
