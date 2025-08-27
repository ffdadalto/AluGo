using System.ComponentModel;

namespace AluGo.Domain
{
    public enum StatusParcela
    {
        [Description("Aberta")]
        spAberta = 'A',
        [Description("Parcial")]
        spParcial = 'P',
        [Description("Quitada")]
        spQuitada = 'Q',
        [Description("Cancelada")]
        spCancelada = 'C',
        [Description("")]
        spNull = '0'
    }

    public enum TipoImovel
    {
        [Description("Casa")]
        tiCasa = 'C',
        [Description("Apartamento")]
        tiApartamento = 'A',
        [Description("Kitnet")]
        tiKitnet = 'K',
        [Description("Comercial")]
        tiComercial = 'M',
        [Description("Outros")]
        tiOutros = 'O',
        [Description("")]
        tiNull = '0'
    }

    public enum TipoPessoa
    {
        [Description("F")]
        tpFisica = 'F',
        [Description("J")]
        tpJuridica = 'J',
        [Description("")]
        tpNull = '0'
    }
}