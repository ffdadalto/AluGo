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
        public async Task<ActionResult<IEnumerable<ImovelDto>>> Get()
        {
            var lista = await _db.Imoveis.OrderBy(x => x.Apelido).ToListAsync();
            return Ok(lista.Select(i => new ImovelDto
            {
                Id = i.Id,
                Apelido = i.Apelido,
                Endereco = i.Endereco,
                Cidade = i.Cidade,
                UF = i.UF,
                Tipo = i.Tipo,
                Ativo = i.Ativo
            }));
        } 

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ImovelDto>> GetById(Guid id)
        {
            var imovel = await _db.Imoveis.FindAsync(id);
            return imovel is null ? NotFound() : new ImovelDto
            {
                Id = imovel.Id,
                Apelido = imovel.Apelido,
                Endereco = imovel.Endereco,
                Cidade = imovel.Cidade,
                UF = imovel.UF,
                Tipo = imovel.Tipo,
                Ativo = imovel.Ativo
            };
        }            


        [HttpPost]
        public async Task<ActionResult<Imovel>> Create(ImovelDto dto)
        {
            var i = new Imovel {
                Apelido = dto.Apelido, 
                Endereco = dto.Endereco, 
                Cidade = dto.Cidade, 
                UF = dto.UF,
                Tipo = dto.Tipo,
                Ativo = dto.Ativo
            };

            _db.Imoveis.Add(i);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = i.Id }, i);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ImovelDto dto)
        {
            var i = await _db.Imoveis.FindAsync(id);
            if (i is null) return NotFound();
            (i.Apelido, i.Endereco, i.Cidade, i.UF, i.Ativo, i.Tipo) = (dto.Apelido, dto.Endereco, dto.Cidade, dto.UF, dto.Ativo, dto.Tipo);
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
