using AluGo.Domain;

namespace AluGo.Dtos
{
    public class LocatarioDto
    {
        public Guid Id { get; set; } 
        public string Nome { get; set; } 
        public string CPF { get; set; } 
        public string RG { get; set; } 
        public TipoPessoa TipoPessoa { get; set; } 
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }          
}
