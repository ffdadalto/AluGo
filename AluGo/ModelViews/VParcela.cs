using AluGo.Data;
using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VParcela
    {
        public Guid Id { get; set; } 
        public Guid ContratoId { get; set; }
        public int Numero { get; set; }
        public string Competencia { get; set; } 
        public DateTime DataVencimento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorMulta { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorOutros { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusParcela Status { get; set; }  
        public DateTime? QuitadaEm { get; set; }
        public DateTime? UltimaEdicao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public Parcela ToModel(AluGoDbContext db)
        {
            var model = db.Parcelas.FirstOrDefault(x => x.Id == this.Id);
            model ??= new Parcela();

            model.ContratoId = this.ContratoId;
            model.Competencia = this.Competencia;
            model.DataVencimento = this.DataVencimento;
            model.ValorBase = this.ValorBase;
            model.ValorDesconto = this.ValorDesconto;
            model.ValorMulta = this.ValorMulta;
            model.ValorJuros = this.ValorJuros;
            model.ValorOutros = this.ValorOutros;
            model.ValorTotal = this.ValorTotal;
            model.Status = this.Status;
            model.QuitadaEm = this.QuitadaEm;
            model.UltimaEdicao = this.UltimaEdicao;
            model.CriadoEm = this.CriadoEm;

            return model;
        }

        public static VParcela FromModel(Parcela model)
        {
            return new VParcela
            {
                Id = model.Id,
                ContratoId = model.ContratoId,
                Competencia = model.Competencia,
                DataVencimento = model.DataVencimento,
                ValorBase = model.ValorBase,
                ValorDesconto = model.ValorDesconto,
                ValorMulta = model.ValorMulta,
                ValorJuros = model.ValorJuros,
                ValorOutros = model.ValorOutros,
                ValorTotal = model.ValorTotal,
                Status = model.Status,
                QuitadaEm = model.QuitadaEm,
                UltimaEdicao = model.UltimaEdicao,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
