using System;
using AluGo.Domain;
using AluGo.Services;
using Xunit;

namespace AluGo.Tests.Services;

public class CalculoParcelaTests
{
    private static Contrato CreateContrato() => new() { MultaPercentual = 2m, JurosAoDiaPercentual = 0.1m };

    [Fact]
    public void AtualizarTotais_OnTimePayment_NoPenaltyOrInterest()
    {
        var contrato = CreateContrato();
        var parcela = new Parcela
        {
            Contrato = contrato,
            ValorBase = 1000m,
            DataVencimento = new DateTime(2024, 1, 10)
        };

        CalculoParcela.AtualizarTotaisParaPagamento(parcela, new DateTime(2024, 1, 10));

        Assert.Equal(0m, parcela.ValorMulta);
        Assert.Equal(0m, parcela.ValorJuros);
        Assert.Equal(1000m, parcela.ValorTotal);
    }

    [Fact]
    public void AtualizarTotais_LatePayment_AppliesPenaltyAndInterest()
    {
        var contrato = CreateContrato();
        var parcela = new Parcela
        {
            Contrato = contrato,
            ValorBase = 1000m,
            DataVencimento = new DateTime(2024, 1, 10)
        };

        CalculoParcela.AtualizarTotaisParaPagamento(parcela, new DateTime(2024, 1, 15));

        Assert.Equal(20m, parcela.ValorMulta);
        Assert.Equal(5m, parcela.ValorJuros);
        Assert.Equal(1025m, parcela.ValorTotal);
    }

    [Fact]
    public void AtualizarTotais_PaymentWithDiscount_UsesReducedBase()
    {
        var contrato = CreateContrato();
        var parcela = new Parcela
        {
            Contrato = contrato,
            ValorBase = 1000m,
            ValorDesconto = 100m,
            DataVencimento = new DateTime(2024, 1, 10)
        };

        CalculoParcela.AtualizarTotaisParaPagamento(parcela, new DateTime(2024, 1, 15));

        Assert.Equal(18m, parcela.ValorMulta);
        Assert.Equal(4.5m, parcela.ValorJuros);
        Assert.Equal(922.5m, parcela.ValorTotal);
    }
}
