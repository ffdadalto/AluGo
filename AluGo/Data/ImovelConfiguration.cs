using AluGo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AluGo.Data
{
    public class ImovelConfiguration: IEntityTypeConfiguration<Imovel>
    {
        public void Configure(EntityTypeBuilder<Imovel> builder)
        {
            // converter enum <-> char
            var tipoConverter = new ValueConverter<TipoImovel, char>(
                v => (char)v,                               // enum -> char
                v => (TipoImovel)v              // char -> enum
            );

            builder.Property(p => p.Tipo)
                .HasConversion(tipoConverter)
                .HasColumnType("char(1)")
                .IsUnicode(false)
                .HasDefaultValue(TipoImovel.Null);           
        }
    }
}
