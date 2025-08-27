using AluGo.Data;
using AluGo.Domain;
using AluGo.Dtos;
using AluGo.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<object>>> Get([FromQuery] Guid? contratoId, [FromQuery] StatusParcela? status, [FromQuery] DateTime? vencimentoDe, [FromQuery] DateTime? vencimentoAte)
        {
            var q = _db.Parcelas
                    .Include(p => p.Contrato).ThenInclude(c => c.Imovel)
                    .Include(p => p.Contrato).ThenInclude(c => c.Locatario)
                    .AsQueryable();


            if (contratoId.HasValue)
                q = q.Where(p => p.ContratoId == contratoId);
            if (status.HasValue) 
                q = q.Where(p => p.Status == status);
            if (vencimentoDe.HasValue) 
                q = q.Where(p => p.DataVencimento >= vencimentoDe.Value.Date);
            if (vencimentoAte.HasValue) 
                q = q.Where(p => p.DataVencimento < vencimentoAte.Value.Date.AddDays(1));


            var list = await q.OrderBy(p => p.DataVencimento).Select(p => new {
                p.Id,
                p.Competencia,
                p.DataVencimento,
                p.ValorBase,
                p.ValorDesconto,
                p.ValorMulta,
                p.ValorJuros,
                p.ValorOutros,
                p.ValorTotal,
                p.Status,
                Contrato = new { p.Contrato.Id, p.Contrato.Imovel.Apelido, p.Contrato.Locatario.Nome }
            }).ToListAsync();

            return Ok(list);
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


        [HttpPost("{id:guid}/recebimentos")]
        public async Task<IActionResult> Receber(Guid id, RecebimentoCreateDto dto)
        {
            var p = await _db.Parcelas
                .Include(x => x.Contrato).ThenInclude(c => c.Imovel)
                .Include(x => x.Contrato).ThenInclude(c => c.Locatario)
                .Include(x => x.Recebimentos)
                .Include(x => x.Recebimentos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (p is null) return NotFound();
            if (p.Status == StatusParcela.spCancelada) return Conflict("Parcela cancelada.");


            // recalcula totais considerando a data de pagamento
            CalculoParcela.AtualizarTotaisParaPagamento(p, dto.DataPagamento);


            var rec = new Recebimento
            {
                Parcela = p,
                DataPagamento = dto.DataPagamento,
                ValorPago = dto.ValorPago,
                MeioPagamento = dto.MeioPagamento,
                Observacao = dto.Observacao
            };
            _db.Recebimentos.Add(rec);


            var totalPago = p.Recebimentos.Sum(r => r.ValorPago) + dto.ValorPago;
            if (totalPago + 0.01m >= p.ValorTotal) // tolerância centavos
            {
                p.Status = StatusParcela.spQuitada;
                p.QuitadaEm = dto.DataPagamento;
            }
            else
            {
                p.Status = StatusParcela.spParcial;
            }


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
    }    
}
    
