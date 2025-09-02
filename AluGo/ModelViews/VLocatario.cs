using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews.Utils;
using System.ComponentModel.DataAnnotations;

namespace AluGo.ModelViews
{
    public class VLocatario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é necessário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'CPF' é necessário.")]
        public string CpfCnpj { get; set; }
        
        public string RG { get; set; }

        [EnumValidation(ErrorMessage = "O campo 'Tipo' é necessário.")]
        public TipoPessoa Tipo { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é necessário.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Telefone' é necessário.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo 'Endereço' é necessário.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo 'Ativo' é necessário.")]
        public bool Ativo { get; set; }

        public DateTime? UltimaEdicao { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Locatario ToModel(AluGoDbContext db)
        {
            var model = db.Locatarios.FirstOrDefault(x => x.Id == this.Id);
            model ??= new Locatario();

            model.Nome = this.Nome;
            model.CpfCnpj = this.CpfCnpj;
            model.RG = this.RG;
            model.Tipo = this.Tipo;
            model.Email = this.Email;
            model.Telefone = this.Telefone;
            model.Endereco = this.Endereco;
            model.Ativo = this.Ativo;
            model.UltimaEdicao = this.UltimaEdicao;
            model.CriadoEm = this.CriadoEm;           

            return model;
        }

        public static VLocatario FromModel(Locatario model)
        {
            return new VLocatario
            {
                Id = model.Id,
                Nome = model.Nome,
                CpfCnpj = model.CpfCnpj,
                RG = model.RG,
                Tipo = model.Tipo,
                Email = model.Email,
                Telefone = model.Telefone,
                Endereco = model.Endereco,
                Ativo = model.Ativo,
                UltimaEdicao = model.UltimaEdicao,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
