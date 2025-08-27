using System.ComponentModel;

namespace AluGo.Domain
{
    public enum StatusParcela
    {
        [Description("Aberta")]
        Aberta = 'A',
        [Description("Parcial")]
        Parcial = 'P',
        [Description("Quitada")]
        Quitada = 'Q',
        [Description("Cancelada")]
        Cancelada = 'C',
        [Description("")]
        Null = '0'
    }

    public enum TipoImovel
    {
        [Description("Casa")]
        Casa = 'C',
        [Description("Apartamento")]
        Apartamento = 'A',
        [Description("Kitnet")]
        Kitnet = 'K',
        [Description("Comercial")]
        Comercial = 'C',
        [Description("Outros")]
        Outros = 'O',
        [Description("")]
        Null = '0'
    }
}