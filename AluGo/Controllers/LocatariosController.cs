using AluGo.Data;
using AluGo.Domain;
using AluGo.Dtos;
using AluGo.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatariosController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public LocatariosController(AluGoDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VLocatario>>> Get()
        {
            var lista = await _db.Locatarios.OrderBy(x => x.Nome).ToListAsync();
            return Ok(lista.Select(i => VLocatario.FromModel(i)));
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VLocatario>> GetById(Guid id)
        {
            var l = await _db.Locatarios.FindAsync(id);
            return l is null ? NotFound() : VLocatario.FromModel(l);
        }


        [HttpPost]
        public async Task<ActionResult<Locatario>> Create(VLocatario view)
        {
            var l = view.ToModel(_db);

            _db.Locatarios.Add(l);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = l.Id }, l);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, VLocatario view)
        {
            var l = await _db.Locatarios.FindAsync(id);
            if (l is null) return NotFound();
            l = view.ToModel(_db);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existeContrato = await _db.Contratos.AnyAsync(c => c.LocatarioId == id && c.Ativo);
            if (existeContrato) return Conflict("Locatário possui contratos ativos.");
            var l = await _db.Locatarios.FindAsync(id);
            if (l is null) return NotFound();
            _db.Locatarios.Remove(l);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
