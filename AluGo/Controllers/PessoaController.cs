using AluGo.Data;
using AluGo.ModelViews;
using AluGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AluGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly AluGoDbContext _db;
        public PessoaController(AluGoDbContext db) => _db = db;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<VLogado>> Login([FromBody] VLogin login)
        {
            var senhaEncriptada = MD5Hash.Criptografar(login.Senha);
            var model = _db.Pessoas.FirstOrDefault(p => p.Email == login.Email && p.Senha == senhaEncriptada);

            if (model is null)
                return BadRequest();

            var token = TokenService.GenerateToken(model);

            return new VLogado
            {
                Pessoa = VPessoa.FromModel(model),
                Token = token
            };
        }
    }
}
