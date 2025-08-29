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
    public class ContratosController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public ContratosController(AluGoDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VContrato>>> Get()
        {
            var lista = await _db.Contratos.ToListAsync();
            return Ok(lista.Select(i => VContrato.FromModel(i)));
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
        public async Task<ActionResult<Contrato>> Create(VContrato view)
        {
            var imovel = await _db.Imoveis.FindAsync(view.ImovelId);
            var locatario = await _db.Locatarios.FindAsync(view.LocatarioId);

            if (imovel is null || locatario is null) 
                return BadRequest("Imóvel/Locatário inválido.");

            var c = view.ToModel(_db);

            var parcelas = FabricaParcelas.GerarParaContrato(c, Math.Max(1, view.MesesGerar)).ToList();
            c.Parcelas = parcelas;

            _db.Contratos.Add(c);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var i = await _db.Contratos.FindAsync(id);
            if (i is null) return NotFound();

            var parcelas = i.Parcelas;

            _db.RemoveRange(parcelas);

            _db.Remove(i);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
