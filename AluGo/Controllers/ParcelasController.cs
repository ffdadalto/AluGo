using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews;
using AluGo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelasController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public ParcelasController(AluGoDbContext db) => _db = db;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VParcela>>> Get()
        {
            var lista = await _db.Parcelas.ToListAsync();
            return Ok(lista.Select(i => VParcela.FromModel(i)));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<object>> GetById(Guid id)
        {
            var p = await _db.Parcelas
            .Include(x => x.Contrato).ThenInclude(c => c.Imovel)
            .Include(x => x.Contrato).ThenInclude(c => c.Locatario)
            .Include(x => x.Recebimentos)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (p is null) return NotFound();
            return Ok(p);
        }


        [HttpPost("recebimentos")]
        public async Task<IActionResult> Receber(VRecebimento view)
        {
            var p = await _db.Parcelas
                            .Include(x => x.Contrato)
                            .Include(x => x.Recebimentos)
                        .Where(x => x.Id == view.ParcelaId)
                        .FirstOrDefaultAsync();            

            if (p is null)
                return NotFound();            

            if (p.Status == StatusParcela.spCancelada)
                return Conflict("Parcela cancelada.");


            decimal totalPago = 0;
            totalPago = p.Recebimentos.Sum(x => x.ValorPago);

            // recalcula totais considerando a data de pagamento
            CalculoParcela.AtualizarTotaisParaPagamento(p, view.DataPagamento);

            var rec = new Recebimento
            {
                Parcela = p,
                DataPagamento = view.DataPagamento,
                ValorPago = view.ValorPago,
                MeioPagamento = view.MeioPagamento,
                Observacao = view.Observacao
            };

            _db.Recebimentos.Add(rec);

            var totalPagoAtual = totalPago + view.ValorPago;

            if (totalPagoAtual + 0.01m >= p.ValorTotal) // tolerância centavos
            {
                p.Status = StatusParcela.spQuitada;
                p.QuitadaEm = view.DataPagamento;
            }
            else
                p.Status = StatusParcela.spParcial;

            await _db.SaveChangesAsync();

            return Ok(new { p.Status, totalPago, p.ValorTotal });
        }

        [HttpPost("{id:guid}/recibo")]
        public async Task<IActionResult> GerarRecibo(Guid id)
        {
            var p = await _db.Parcelas
                .Include(x => x.Contrato).ThenInclude(c => c.Imovel)
                .Include(x => x.Contrato).ThenInclude(c => c.Locatario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p is null) return NotFound();
            if (p.Status != StatusParcela.spQuitada)
                return Conflict("Somente parcelas quitadas geram recibo.");

            var pdf = ReciboPdf.Gerar(p);
            return File(pdf, "application/pdf", $"recibo-{p.Competencia}-{p.Id}.pdf");
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<VParcelaLista>>> GetLista()
        {
            var collection = await _db.Parcelas
                                    .OrderBy(x => x.DataVencimento)
                                    .Include(x => x.Recebimentos)                                    
                                    .Include(x => x.Contrato.Locatario)
                                    .Include(x => x.Contrato.Imovel)
                                    .ToListAsync();

            if(collection is null || collection.Count == 0)
                return NoContent();

            var lista = collection.Select(i => new VParcelaLista
            {
                Id = i.Id,
                ContratoId = i.ContratoId,
                ContratoNumero = i.Contrato.Numero,
                LocatarioNome = i.Contrato.Locatario.Nome,
                ImovelNome = i.Contrato.Imovel.Apelido,
                Competencia = i.Competencia,
                DataVencimento = i.DataVencimento,
                ValorTotal = i.ValorTotal,
                Status = i.Status,
                QuitadaEm = i.QuitadaEm,
                ValorTotalPago = i.Recebimentos.Sum(r => r.ValorPago)
            }).ToList();

            return Ok(lista);
        }
    }
}

