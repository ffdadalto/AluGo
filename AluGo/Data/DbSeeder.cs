using AluGo.Domain;
using AluGo.Services;
using Microsoft.EntityFrameworkCore;
namespace AluGo.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AluGoDbContext db)
    {
        // aplica migrations
        await db.Database.MigrateAsync();
        // se já houver dados, não semear de novo
        if (await db.Contratos.AnyAsync()) return;

        var imovel = new Imovel
        {
            Apelido = "Apto Centro",
            Endereco = "Rua A, 123",
            Cidade = "Serra",
            UF = "ES"
        };

        var loc = new Locatario
        {
            Nome = "João da Silva",
            CPF = "000.111.222-33",
            Email = "joao@email.com",
            Telefone = "(27) 99999-0000",
            Endereco = "Av. Principal, 500"
        };

        var contrato = new Contrato
        {
            Imovel = imovel,
            Locatario = loc,
            DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
            DiaVencimento = 5,
            ValorAluguel = 1500m,
            DescontoAteVencimento = 0m,
            MultaPercentual = 2m,
            JurosAoDiaPercentual = 0.0333m,
            ReajusteIndice = "IPCA",
            ReajustePeriodicidadeMeses = 12,
            Ativo = true
        };

        var parcelas = FabricaParcelas.GerarParaContrato(contrato, 12).ToList();

        contrato.Parcelas = parcelas;

        // Marca a primeira parcela como quitada na data de vencimento
        var p1 = parcelas.First();
        CalculoParcela.AtualizarTotaisParaPagamento(p1, p1.DataVencimento);

        var rec = new Recebimento
        {
            Parcela = p1,
            DataPagamento = p1.DataVencimento,
            ValorPago = p1.ValorTotal,
            MeioPagamento = "PIX",
            Observacao = "Pagamento integral"
        };

        db.Recebimentos.Add(rec);

        p1.Status = StatusParcela.spQuitada;

        p1.QuitadaEm = p1.DataVencimento;

        db.Contratos.Add(contrato);

        await db.SaveChangesAsync();
    }
}
