using AluGo.Data;
using AluGo.Domain;
using AluGo.ModelViews.Utils;
using System.ComponentModel.DataAnnotations;

namespace AluGo.ModelViews
{
    public class VImovel
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo 'Apelido' é necessário.")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "O campo 'Apelido' é necessário.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo 'Apelido' é necessário.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo 'Apelido' é necessário.")]
        public string UF { get; set; }

        [EnumValidation(ErrorMessage = "O campo 'Tipo' é necessário.")]
        public TipoImovel Tipo { get; set; }

        [Required(ErrorMessage = "O campo 'Apelido' é necessário.")]
        public bool Ativo { get; set; }

        public DateTime CriadoEm { get; set; }

        public Imovel ToModel(AluGoDbContext db)
        {
            var model = db.Imoveis.FirstOrDefault(x => x.Id == this.Id);
            if (model == null)
                model = new Imovel();

            model.Apelido = this.Apelido;
            model.Endereco = this.Endereco;
            model.Cidade = this.Cidade;
            model.UF = this.UF;
            model.Tipo = this.Tipo;
            model.Ativo = this.Ativo;
            model.CriadoEm = this.CriadoEm;

            return model;
        }

        public static VImovel FromModel(Imovel model)
        {
            return new VImovel
            {

                Id = model.Id,
                Apelido = model.Apelido,
                Endereco = model.Endereco,
                Cidade = model.Cidade,
                UF = model.UF,
                Tipo = model.Tipo,
                Ativo = model.Ativo,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
