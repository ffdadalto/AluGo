using AluGo.Domain;
using System.Diagnostics.Contracts;

namespace AluGo.Services
{
    public static class FabricaParcelas
    {
        public static IEnumerable<Parcela> GerarParaContrato(Contrato c, int meses)
        {
            var inicio = new DateTime(c.DataInicio.Year, c.DataInicio.Month, 1);
            for (int i = 0; i < meses; i++)
            {
                var comp = inicio.AddMonths(i);
                var dia = Math.Clamp(c.DiaVencimento, 1, DateTime.DaysInMonth(comp.Year, comp.Month));
                var venc = new DateTime(comp.Year, comp.Month, dia);
                yield return new Parcela
                {
                    Contrato = c,
                    Competencia = $"{comp:MM-yyyy}",
                    DataVencimento = venc,
                    ValorBase = c.ValorAluguel,
                    ValorDesconto = c.DescontoAteVencimento ?? 0m,
                    ValorMulta = 0m,
                    ValorJuros = 0m,
                    ValorOutros = 0m,
                    ValorTotal = Math.Round(c.ValorAluguel - (c.DescontoAteVencimento ?? 0m), 2)
                };
            }
        }
    }
}
