using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews.Utils;
using System.ComponentModel.DataAnnotations;

namespace AluGo.ModelViews
{
    public class VLocatario
    {
        public Guid Id { get; set; } 

        public string Nome { get; set; } 

        public string CPF { get; set; } 

        public string RG { get; set; }

        [EnumValidation(ErrorMessage = "O campo 'Tipo' é necessário.")]
        public TipoPessoa Tipo { get; set; } 

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }


        [Required(ErrorMessage = "O campo 'Ativo' é necessário.")]
        public bool Ativo { get; set; }

        public DateTime CriadoEm { get; set; }

        public Locatario ToModel(AluGoDbContext db)
        {
            var model = db.Locatarios.FirstOrDefault(x => x.Id == this.Id);
            model ??= new Locatario();

            model.Nome = this.Nome;
            model.CPF = this.CPF;
            model.RG = this.RG;
            model.Tipo = this.Tipo;
            model.Email = this.Email;
            model.Telefone = this.Telefone;
            model.Endereco = this.Endereco;
            model.CriadoEm = this.CriadoEm;           

            return model;
        }

        public static VLocatario FromModel(Locatario model)
        {
            return new VLocatario
            {
                Id = model.Id,
                Nome = model.Nome,
                CPF = model.CPF,
                RG = model.RG,
                Tipo = model.Tipo,
                Email = model.Email,
                Telefone = model.Telefone,
                Endereco = model.Endereco,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
