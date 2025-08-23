using AluGo.Domain;

namespace AluGo.Services
{
    public static class CalculoParcela
    {
        public static void AtualizarTotaisParaPagamento(Parcela p, DateTime dataPagamento)
        {
            if (p.Contrato is null)
                throw new InvalidOperationException("Parcela precisa carregar o Contrato para calcular multa/juros.");


            var baseCalculo = Math.Max(0m, p.ValorBase - p.ValorDesconto);
            var atrasoDias = (dataPagamento.Date - p.DataVencimento.Date).Days;
            p.ValorMulta = atrasoDias > 0 ? Math.Round(baseCalculo * (p.Contrato.MultaPercentual / 100m), 2) : 0m;
            p.ValorJuros = atrasoDias > 0 ? Math.Round(baseCalculo * (p.Contrato.JurosAoDiaPercentual / 100m) * atrasoDias, 2) : 0m;
            p.ValorTotal = Math.Round(baseCalculo + p.ValorMulta + p.ValorJuros + p.ValorOutros, 2);
        }
    }
}
