using AluGo.Domain;

namespace AluGo.Dtos
{
    public record ImovelCreateDto(string Apelido, string Endereco, string Cidade, string UF, TipoImovel Tipo);
    public record ImovelUpdateDto(string Apelido, string Endereco, string Cidade, string UF, TipoImovel Tipo, bool Ativo);
}
