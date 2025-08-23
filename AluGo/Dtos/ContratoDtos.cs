namespace AluGo.Dtos
{        
    public record ContratoCreateDto(
        Guid ImovelId,
        Guid LocatarioId,
        DateTime DataInicio,
        DateTime? DataFim,
        byte DiaVencimento,
        decimal ValorAluguel,
        decimal? DescontoAteVencimento,
        decimal MultaPercentual,
        decimal JurosAoDiaPercentual,
        string ReajusteIndice,
        byte ReajustePeriodicidadeMeses,
        string? Observacoes,
        int MesesGerar // ex.: 12
        );

    public record ReajusteCreateDto(DateTime DataBase, decimal Percentual, string? Observacao);
}
