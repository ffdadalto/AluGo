using AluGo.Data;
using AluGo.Domain;

namespace AluGo.ModelViews
{
    public class VContrato
    {
        public Guid Id { get; set; } 
        public Guid ImovelId { get; set; }
        public Guid LocatarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public byte DiaVencimento { get; set; } 
        public decimal ValorAluguel { get; set; }
        public decimal? DescontoAteVencimento { get; set; }
        public decimal MultaPercentual { get; set; } 
        public decimal JurosAoDiaPercentual { get; set; } 
        public string ReajusteIndice { get; set; } 
        public byte ReajustePeriodicidadeMeses { get; set; } 
        public DateTime? ReajusteUltimaData { get; set; }
        public int MesesGerar { get; set; }
        public string Observacoes { get; set; }
        public bool Ativo { get; set; } 
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Contrato ToModel(AluGoDbContext db)
        {
            var model = db.Contratos.FirstOrDefault(x => x.Id == this.Id);
            model ??= new Contrato();
            
            model.ImovelId = this.ImovelId;
            model.LocatarioId = this.LocatarioId;
            model.DataInicio = this.DataInicio;
            model.DataFim = this.DataFim;
            model.DiaVencimento = this.DiaVencimento;
            model.ValorAluguel = this.ValorAluguel;
            model.DescontoAteVencimento = this.DescontoAteVencimento;
            model.MultaPercentual = this.MultaPercentual;
            model.JurosAoDiaPercentual = this.JurosAoDiaPercentual;
            model.ReajusteIndice = this.ReajusteIndice;
            model.ReajustePeriodicidadeMeses = this.ReajustePeriodicidadeMeses;
            model.ReajusteUltimaData = this.ReajusteUltimaData;
            model.Observacoes = this.Observacoes;
            model.Ativo = this.Ativo;
            model.CriadoEm = this.CriadoEm;

            return model;
        }

        public static VContrato FromModel(Contrato model)
        {
            return new VContrato
            {
                Id = model.Id,
                ImovelId = model.ImovelId,
                LocatarioId = model.LocatarioId,
                DataInicio = model.DataInicio,
                DataFim = model.DataFim,
                DiaVencimento = model.DiaVencimento,
                ValorAluguel = model.ValorAluguel,
                DescontoAteVencimento = model.DescontoAteVencimento,
                MultaPercentual = model.MultaPercentual,
                JurosAoDiaPercentual = model.JurosAoDiaPercentual,
                ReajusteIndice = model.ReajusteIndice,
                ReajustePeriodicidadeMeses = model.ReajustePeriodicidadeMeses,
                ReajusteUltimaData = model.ReajusteUltimaData,
                Observacoes = model.Observacoes,
                Ativo = model.Ativo,
                CriadoEm = model.CriadoEm
            };
        }
    }
}
