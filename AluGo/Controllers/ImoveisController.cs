using AluGo.Data;
using AluGo.ModelViews;
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
        public async Task<ActionResult<IEnumerable<VImovel>>> Get()
        {
            var lista = await _db.Imoveis.OrderBy(x => x.Apelido).ToListAsync();
            return Ok(lista.Select(i => VImovel.FromModel(i)));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VImovel>> GetById(Guid id)
        {
            var imovel = await _db.Imoveis.FindAsync(id);
            return imovel is null ? NotFound() : VImovel.FromModel(imovel);
        }


        [HttpPost]
        public async Task<ActionResult<VImovel>> Create(VImovel view)
        {
            var i = view.ToModel(_db);

            _db.Imoveis.Add(i);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = i.Id }, i);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, VImovel view)
        {
            var i = await _db.Imoveis.FindAsync(id);
            if (i is null) return NotFound();
            i = view.ToModel(_db);
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
