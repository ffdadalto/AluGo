using System.Diagnostics.Contracts;

namespace AluGo.Domain
{
    public class Locatario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty; 
        public string RG { get; set; } = string.Empty; 
        public TipoPessoa Tipo { get; set; } = TipoPessoa.tpFisica;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.Now;


        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
