using AluGo.Domain;

namespace AluGo.Dtos
{
    public class ImovelDto
    {
        public Guid Id { get; set; } 
        public string Apelido { get; set; } 
        public string Endereco { get; set; } 
        public string Cidade { get; set; }
        public string UF { get; set; } 
        public TipoImovel Tipo { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
