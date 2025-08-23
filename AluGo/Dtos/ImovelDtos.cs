namespace AluGo.Dtos
{
    public record ImovelCreateDto(string Apelido, string Endereco, string Cidade, string UF);
    public record ImovelUpdateDto(string Apelido, string Endereco, string Cidade, string UF, bool Ativo);
}
