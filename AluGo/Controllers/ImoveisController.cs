using AluGo.Data;
using AluGo.Domain;
using AluGo.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public ImoveisController(AluGoDbContext db) => _db = db;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> Get([FromQuery] bool? ativo)
        {
            var q = _db.Imoveis.AsQueryable();
            if (ativo.HasValue) q = q.Where(x => x.Ativo == ativo);
            return await q.OrderBy(x => x.Apelido).ToListAsync();
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Imovel>> GetById(Guid id)
        => await _db.Imoveis.FindAsync(id) is { } i ? Ok(i) : NotFound();


        [HttpPost]
        public async Task<ActionResult<Imovel>> Create(ImovelCreateDto dto)
        {
            var i = new Imovel { Apelido = dto.Apelido, Endereco = dto.Endereco, Cidade = dto.Cidade, UF = dto.UF };
            _db.Imoveis.Add(i);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = i.Id }, i);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ImovelUpdateDto dto)
        {
            var i = await _db.Imoveis.FindAsync(id);
            if (i is null) return NotFound();
            (i.Apelido, i.Endereco, i.Cidade, i.UF, i.Ativo) = (dto.Apelido, dto.Endereco, dto.Cidade, dto.UF, dto.Ativo);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var i = await _db.Imoveis.FindAsync(id);
            if (i is null) return NotFound();
            i.Ativo = false;
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
