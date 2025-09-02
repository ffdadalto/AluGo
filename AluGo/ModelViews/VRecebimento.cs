using AluGo.Data;
using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VRecebimento
    {

        public Guid Id { get; set; } 
        public Guid ParcelaId { get; set; } 
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string Observacao { get; set; }
        public string MeioPagamento { get; set; }
        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Recebimento ToModel(AluGoDbContext db)
        {
            var model = db.Recebimentos.FirstOrDefault(x => x.Id == this.Id);
            model ??= new Recebimento();

            model.ParcelaId = this.ParcelaId;
            model.DataPagamento = this.DataPagamento;
            model.ValorPago = this.ValorPago;
            model.Observacao = this.Observacao;
            model.MeioPagamento = this.MeioPagamento;
            model.UltimaEdicao = this.UltimaEdicao;
            model.CriadoEm = this.CriadoEm;

            return model;
        }

        public static VRecebimento FromModel(Recebimento model)
        {
            return new VRecebimento
            {
                Id = model.Id,
                ParcelaId = model.ParcelaId,
                DataPagamento = model.DataPagamento,
                ValorPago = model.ValorPago,
                Observacao = model.Observacao,
                MeioPagamento = model.MeioPagamento,
                UltimaEdicao = model.UltimaEdicao,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
