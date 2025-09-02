using AluGo.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.IO;

namespace AluGo.Services
{
    public static class ReciboPdf
    {
        private static readonly CultureInfo PtBr = new("pt-BR");
        private static string Moeda(decimal v) => v.ToString("C", PtBr);

        public static byte[] Gerar(Parcela p)
        {
            var prevCulture = CultureInfo.CurrentCulture;
            var prevUiCulture = CultureInfo.CurrentUICulture;
            CultureInfo.CurrentCulture = PtBr;
            CultureInfo.CurrentUICulture = PtBr;

            try
            {
                var primary = Colors.Grey.Darken3;
                var textDark = Colors.Grey.Darken4;
                var textMuted = Colors.Grey.Darken1;
                var panelBg = Colors.Grey.Lighten4;
                var border = Colors.Grey.Lighten1;

                var quitada = p.QuitadaEm;
                var reciboCodigo = $"{p.Competencia} - {p.DataVencimento:MM/yyyy}";

                // >>> CONTROLES DE TAMANHO DA ASSINATURA (em pontos)
                const float assinaturaLargura = 200f;
                const float assinaturaAltura = 40f;

                // Carrega a imagem da assinatura, se disponível
                byte[]? assinaturaBytes = null;
                try
                {
                    var assinaturaPath = Path.Combine(
                        AppContext.BaseDirectory,
                        "Resources", "Assinaturas", "assinatura-locador.png"
                    );

                    if (File.Exists(assinaturaPath))
                        assinaturaBytes = File.ReadAllBytes(assinaturaPath);
                }
                catch { /* fallback sem imagem */ }

                return Document.Create(doc =>
                {
                    doc.Page(page =>
                    {
                        page.Size(PageSizes.A5);
                        page.Margin(16);
                        page.DefaultTextStyle(x => x.FontSize(10).FontColor(textDark));

                        page.Content().Column(col =>
                        {
                            col.Spacing(10);

                            col.Item().Container()
                                .Background(Colors.White)
                                .Border(1).BorderColor(border)
                                .Padding(14)
                                .Column(card =>
                                {
                                    card.Spacing(10);

                                    // Cabeçalho
                                    card.Item().Row(row =>
                                    {
                                        row.RelativeItem().Column(ch =>
                                        {
                                            ch.Item().Text(t =>
                                            {
                                                t.Span("RECIBO DE ALUGUEL")
                                                 .FontSize(16)
                                                 .SemiBold()
                                                 .FontColor(primary);
                                            });
                                            ch.Item().Text($"Recibo n.º {reciboCodigo}")
                                                     .FontSize(9)
                                                     .FontColor(textMuted);
                                        });
                                        // row.ConstantItem(56).Height(40).Image(logoBytes).FitArea(); // opcional
                                    });

                                    // Divisor
                                    card.Item().Container().Height(1).Background(border);

                                    // Infos
                                    card.Item().Column(info =>
                                    {
                                        info.Spacing(4);

                                        info.Item().Text(t =>
                                        {
                                            t.Span("Recebi de ").SemiBold();
                                            t.Span(p.Contrato.Locatario.Nome);
                                        });

                                        info.Item().Text(t =>
                                        {
                                            t.Span("A quantia de ").SemiBold();
                                            t.Span(Moeda(p.ValorTotal)).SemiBold().FontColor(primary);
                                        });

                                        info.Item().Text($"Ref.: Aluguel competência {p.Competencia} do imóvel {p.Contrato.Imovel.Nome}.");

                                        info.Item().Text($"Venc.: {p.DataVencimento:dd/MM/yyyy}  |  Pago em: {quitada.Value:dd/MM/yyyy}")
                                                   .FontColor(textMuted);
                                    });

                                    // Resumo financeiro
                                    card.Item().Container()
                                        .Background(panelBg)
                                        .Border(1).BorderColor(border)
                                        .Padding(10)
                                        .Column(box =>
                                        {
                                            box.Spacing(4);

                                            box.Item().Text("Resumo financeiro").SemiBold();

                                            box.Item().Row(r =>
                                            {
                                                r.RelativeItem().Text($"Valor base: {Moeda(p.ValorBase)}");
                                                r.RelativeItem().AlignRight().Text($"Desconto: {Moeda(p.ValorDesconto)}");
                                            });

                                            box.Item().Row(r =>
                                            {
                                                r.RelativeItem().Text($"Multa: {Moeda(p.ValorMulta)}");
                                                r.RelativeItem().AlignRight().Text($"Juros: {Moeda(p.ValorJuros)}");
                                            });

                                            box.Item().Container().PaddingTop(4)
                                               .Background(primary)
                                               .Padding(8)
                                               .AlignRight()
                                               .Text($"TOTAL PAGO {Moeda(p.ValorTotal)}")
                                               .FontSize(12).SemiBold().FontColor(Colors.White);
                                        });

                                    // Observações
                                    card.Item().Text("Este recibo comprova o pagamento da parcela mencionada acima. Documento emitido para fins de quitação da obrigação.")
                                               .FontSize(8.5f)
                                               .FontColor(textMuted);

                                    // Assinatura com imagem (usando as variáveis de tamanho)
                                    card.Item().Column(sig =>
                                    {
                                        sig.Spacing(2);

                                        if (assinaturaBytes is not null)
                                        {
                                            sig.Item().AlignCenter().Container()
                                                .Width(assinaturaLargura)       // >>> usa a largura controlada
                                                .PaddingBottom(2)
                                                .Column(x =>
                                                {
                                                    x.Spacing(2);

                                                    // >>> caixa da imagem com largura e altura controladas
                                                    x.Item().AlignCenter()
                                                        .Width(assinaturaLargura)
                                                        .Height(assinaturaAltura)
                                                        .Image(assinaturaBytes).FitArea();

                                                    // linha inferior
                                                    x.Item().Container()
                                                        .BorderBottom(1).BorderColor(border);
                                                });

                                            sig.Item().AlignCenter().Text("Assinatura do Locador")
                                               .FontSize(9).FontColor(textMuted);
                                        }
                                        else
                                        {
                                            // Fallback: linha e legenda (mantém a mesma largura)
                                            sig.Item().AlignCenter().Container()
                                                .Width(assinaturaLargura)       // >>> usa a largura controlada
                                                .BorderBottom(1).BorderColor(border)
                                                .PaddingBottom(2);

                                            sig.Item().AlignCenter().Text("Assinatura do Locador")
                                               .FontSize(9).FontColor(textMuted);
                                        }
                                    });

                                    // Data
                                    card.Item().Text($"Emitido em {DateTime.Now:dd 'de' MMMM 'de' yyyy}")
                                               .FontSize(8.5f).FontColor(textMuted).AlignRight();
                                });                            
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
