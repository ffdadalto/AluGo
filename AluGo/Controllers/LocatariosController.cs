using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<VLocatario>>> Get()
        {
            var lista = await _db.Locatarios.OrderBy(x => x.Nome).ToListAsync();
            return Ok(lista.Select(i => VLocatario.FromModel(i)));
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<VLocatario>> GetById(Guid id)
        {
            var locatario = await _db.Locatarios.FindAsync(id);
            return locatario is null ? NotFound() : VLocatario.FromModel(locatario);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VLocatario>> Create(VLocatario view)
        {
            var locatario = view.ToModel(_db);

            _db.Locatarios.Add(locatario);
            await _db.SaveChangesAsync();
            return Ok(VLocatario.FromModel(locatario));
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, VLocatario view)
        {
            var locatario = await _db.Locatarios.FindAsync(id);

            if (locatario is null) 
                return NotFound();

            locatario = view.ToModel(_db);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existeContrato = await _db.Contratos
                                    .AnyAsync(c => c.LocatarioId == id && c.Ativo);

            if (existeContrato) 
                return Conflict("Locatário possui contratos ativos.");

            var locatario = await _db.Locatarios.FindAsync(id);

            if (locatario is null) 
                return NotFound();

            locatario.Ativo = false;

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
