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
        public async Task<ActionResult<IEnumerable<LocatarioDto>>> Get()
        {
            var lista = await _db.Locatarios.OrderBy(x => x.Nome).ToListAsync();
            return Ok(lista.Select(i => new LocatarioDto
            {
                Id = i.Id,
                Nome = i.Nome,
                CPF = i.CPF,
                RG = i.RG,
                TipoPessoa = i.TipoPessoa,
                Email = i.Email,
                Telefone = i.Telefone,
                Endereco = i.Endereco
            }));  
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LocatarioDto>> GetById(Guid id)
        {
            var l = await _db.Locatarios.FindAsync(id);
            return l is null ? NotFound() : new LocatarioDto
            {
                Id = l.Id,
                Nome = l.Nome,
                CPF = l.CPF,
                RG = l.RG,
                TipoPessoa = l.TipoPessoa,
                Email = l.Email,
                Telefone = l.Telefone,
                Endereco = l.Endereco
            };
        }


        [HttpPost]
        public async Task<ActionResult<Locatario>> Create(LocatarioDto dto)
        {
            var l = new Locatario
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                RG = dto.RG,
                TipoPessoa = dto.TipoPessoa,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Endereco = dto.Endereco
            };

            _db.Locatarios.Add(l);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = l.Id }, l);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, LocatarioDto dto)
        {
            var l = await _db.Locatarios.FindAsync(id);
            if (l is null) return NotFound();
            (l.Nome, l.CPF, l.RG, l.TipoPessoa, l.Email, l.Telefone, l.Endereco) = (dto.Nome, dto.CPF, dto.RG, dto.TipoPessoa, dto.Email, dto.Telefone, dto.Endereco);
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
