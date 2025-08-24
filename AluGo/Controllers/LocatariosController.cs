using AluGo.Data;
using AluGo.Domain;
using AluGo.Dtos;
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
        public async Task<ActionResult<IEnumerable<Locatario>>> Get()
        => await _db.Locatarios.OrderBy(x => x.Nome).ToListAsync();


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Locatario>> GetById(Guid id)
        => await _db.Locatarios.FindAsync(id) is { } l ? Ok(l) : NotFound();


        [HttpPost]
        public async Task<ActionResult<Locatario>> Create(LocatarioCreateDto dto)
        {
            var l = new Locatario { 
                Nome = dto.Nome, CPF = dto.CPF, 
                Email = dto.Email, 
                Telefone = dto.Telefone, 
                Endereco = dto.Endereco 
            };

            _db.Locatarios.Add(l);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = l.Id }, l);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, LocatarioUpdateDto dto)
        {
            var l = await _db.Locatarios.FindAsync(id);
            if (l is null) return NotFound();
            (l.Nome, l.CPF, l.Email, l.Telefone, l.Endereco) = (dto.Nome, dto.CPF, dto.Email, dto.Telefone, dto.Endereco);
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
