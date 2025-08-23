using AluGo.Data;
using AluGo.Domain;
using AluGo.Dtos;
using AluGo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public ContratosController(AluGoDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get([FromQuery] bool? ativo, [FromQuery] Guid? imovelId, [FromQuery] Guid? locatarioId)
        {
            var q = _db.Contratos
                        .Include(c => c.Imovel)
                        .Include(c => c.Locatario)
                        .AsQueryable();

            if (ativo.HasValue)
                q = q.Where(c => c.Ativo == ativo);

            if (imovelId.HasValue)
                q = q.Where(c => c.ImovelId == imovelId);

            if (locatarioId.HasValue)
                q = q.Where(c => c.LocatarioId == locatarioId);

            var list = await q
                        .OrderByDescending(c => c.CriadoEm)
                        .Select(c => new
                        {
                            c.Id,
                            c.DataInicio,
                            c.DataFim,
                            c.DiaVencimento,
                            c.ValorAluguel,
                            c.Ativo,
                            Imovel = c.Imovel.Apelido,
                            Locatario = c.Locatario.Nome
                        }).ToListAsync();

            return Ok(list);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Contrato>> GetById(Guid id)
        {
            var c = await _db.Contratos
                            .Include(x => x.Imovel)
                            .Include(x => x.Locatario)
                            .Include(x => x.Parcelas)
                            .FirstOrDefaultAsync(x => x.Id == id);

            return c is null ? NotFound() : Ok(c);
        }


        [HttpPost]
        public async Task<ActionResult<Contrato>> Create(ContratoCreateDto dto)
        {
            var imovel = await _db.Imoveis.FindAsync(dto.ImovelId);
            var locatario = await _db.Locatarios.FindAsync(dto.LocatarioId);

            if (imovel is null || locatario is null) 
                return BadRequest("Imóvel/Locatário inválido.");

            var c = new Contrato
            {
                ImovelId = dto.ImovelId,
                LocatarioId = dto.LocatarioId,
                DataInicio = dto.DataInicio.Date,
                DataFim = dto.DataFim?.Date,
                DiaVencimento = dto.DiaVencimento,
                ValorAluguel = dto.ValorAluguel,
                DescontoAteVencimento = dto.DescontoAteVencimento,
                MultaPercentual = dto.MultaPercentual,
                JurosAoDiaPercentual = dto.JurosAoDiaPercentual,
                ReajusteIndice = dto.ReajusteIndice,
                ReajustePeriodicidadeMeses = dto.ReajustePeriodicidadeMeses,
                Observacoes = dto.Observacoes,
                Ativo = true
            };

            var parcelas = FabricaParcelas.GerarParaContrato(c, Math.Max(1, dto.MesesGerar)).ToList();
            c.Parcelas = parcelas;

            _db.Contratos.Add(c);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
        }
    }
}
