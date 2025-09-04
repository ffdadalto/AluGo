using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews;
using AluGo.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<VContrato>>> Get()
        {
            var lista = await _db.Contratos.ToListAsync();
            return Ok(lista.Select(i => VContrato.FromModel(i)));
        }


        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<VContrato>> GetById(Guid id)
        {
            var c = await _db.Contratos.FirstOrDefaultAsync(x => x.Id == id);
            return c is null ? NotFound() : Ok(VContrato.FromModel(c));
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VContrato>> Create(VContrato view) 
        { 
            var numUltimoContrato = await _db.Contratos
                                        .OrderByDescending(c => c.Numero)
                                        .Select(c => c.Numero)
                                        .FirstOrDefaultAsync();
            view.Numero = numUltimoContrato + 1;
            var contrato = view.ToModel(_db);
            _db.Contratos.Add(contrato);
            await _db.SaveChangesAsync();

            var parcelas = FabricaParcelas.GerarParaContrato(contrato, Math.Max(1, view.MesesGerar)).ToList();
            _db.Parcelas.AddRange(parcelas);

            await _db.SaveChangesAsync();

            return Ok(contrato);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, VContrato view)
        {
            var contrato = await _db.Contratos.FindAsync(id);

            if (contrato is null)
                return NotFound();

            contrato = view.ToModel(_db);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var contrato = await _db.Contratos
                            .Include(c => c.Parcelas)
                                .ThenInclude(p => p.Recebimentos)
                            .FirstOrDefaultAsync(c => c.Id == id);

            if (contrato is null) return NotFound();

            var parcelas = contrato.Parcelas;
            var recebimentos = parcelas.SelectMany(p => p.Recebimentos);

            _db.RemoveRange(parcelas);
            _db.RemoveRange(recebimentos);

            _db.Remove(contrato);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("lista")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VContratoLista>>> GetLista()
        {
            var collection = await _db.Contratos
                                    .OrderByDescending(x => x.Numero)
                                    .Include(x => x.Imovel)
                                    .Include(x => x.Locatario)
                                    .Include(x => x.Parcelas)
                                    .ToListAsync();

            if (collection is null || collection.Count == 0)
                return NoContent();

            var lista = collection.Select(i => new VContratoLista
            {
                Id = i.Id,
                ImovelId = i.ImovelId,
                ImovelNome = i.Imovel?.Apelido ?? "",
                LocatarioId = i.LocatarioId,
                LocatarioNome = i.Locatario?.Nome ?? "",
                Numero = i.Numero,
                DataInicio = i.DataInicio,
                DataFim = i.DataFim,
                ValorAluguel = i.ValorAluguel,
                Ativo = i.Ativo,
                ValorContrato = i.Parcelas.Sum(p => p.ValorTotal)

            }).ToList();

            return Ok(lista);
        }
    }
}
