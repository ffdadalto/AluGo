namespace AluGo.Dtos
{    
    public record LocatarioCreateDto(string Nome, string Documento, string? Email, string? Telefone, string? Endereco);
    public record LocatarioUpdateDto(string Nome, string Documento, string? Email, string? Telefone, string? Endereco);
}
