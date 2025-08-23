using AluGo.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace AluGo.Services
{
    public static class ReciboPdf
    {
        private static readonly CultureInfo PtBr = new("pt-BR");
        private static string Moeda(decimal v) => v.ToString("C", PtBr);

        public static byte[] Gerar(Parcela p)
        {
            // Alternativa segura: ajustar a cultura atual apenas durante a geração e restaurar em seguida
            var prevCulture = CultureInfo.CurrentCulture;
            var prevUiCulture = CultureInfo.CurrentUICulture;
            CultureInfo.CurrentCulture = PtBr;
            CultureInfo.CurrentUICulture = PtBr;
            try
            {
                return Document.Create(doc =>
                {
                    doc.Page(pag =>
                    {
                        pag.Size(PageSizes.A5);
                        pag.Margin(20);
                        pag.DefaultTextStyle(x => x.FontSize(11));
                        pag.Content().Column(col =>
                        {
                            col.Item().Text("RECIBO DE ALUGUEL").FontSize(16).SemiBold().AlignCenter();
                            col.Spacing(8);


                            col.Item().Text($"Recebi de {p.Contrato.Locatario.Nome} a quantia de {Moeda(p.ValorTotal)}");
                            col.Item().Text($"Ref.: aluguel {p.Competencia} do imóvel {p.Contrato.Imovel.Apelido}.");
                            var quitada = p.QuitadaEm ?? DateTime.Now;
                            col.Item().Text($"Venc.: {p.DataVencimento:dd/MM/yyyy} | Pago em: {quitada:dd/MM/yyyy}");


                            col.Spacing(16);
                            col.Item().Row(r =>
                            {
                                r.RelativeItem().Text($"Valor base: {Moeda(p.ValorBase)}");
                                r.RelativeItem().Text($"Desc.: {Moeda(p.ValorDesconto)}");
                            });
                            col.Item().Row(r =>
                            {
                                r.RelativeItem().Text($"Multa: {Moeda(p.ValorMulta)}");
                                r.RelativeItem().Text($"Juros: {Moeda(p.ValorJuros)}");
                            });
                            col.Item().Text($"Total: {Moeda(p.ValorTotal)}").SemiBold();


                            col.Spacing(30);
                            col.Item().AlignCenter().Text("________________________________________");
                            col.Item().AlignCenter().Text("Assinatura do Locador");
                        });
                    });
                }).GeneratePdf();
            }
            finally
            {
                CultureInfo.CurrentCulture = prevCulture;
                CultureInfo.CurrentUICulture = prevUiCulture;
            }
        }
    }
}
