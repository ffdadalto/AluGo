using AluGo.Data;
using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VPessoa
    {
        public Guid Id { get; set; }         
        public string Email { get; set; } 
        public string Senha { get; set; } 
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Pessoa ToModel(AluGoDbContext db)
        {
            var model = db.Pessoas.Find(this.Id);
            model ??= new Pessoa();
                        
            model.Email = this.Email;
            model.Senha = this.Senha;
            model.Ativo = this.Ativo;
            model.CriadoEm = this.CriadoEm;
            return model;
        }

        public static VPessoa FromModel(Pessoa model)
        {
            return new VPessoa
            {
                Id = model.Id,                
                Email = model.Email,
                Senha = model.Senha,
                Ativo = model.Ativo,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
