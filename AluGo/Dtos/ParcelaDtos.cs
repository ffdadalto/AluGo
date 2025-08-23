using AluGo.Domain;

namespace AluGo.Dtos
{
    public record ParcelaFiltroDto(Guid? ContratoId, StatusParcela? Status, DateTime? VencimentoDe, DateTime? VencimentoAte);
    public record RecebimentoCreateDto(DateTime DataPagamento, decimal ValorPago, string MeioPagamento, string? Observacao);
}
