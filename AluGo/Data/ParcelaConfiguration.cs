using AluGo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AluGo.Data
{
    public class ParcelaConfiguration: IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            // converter enum <-> char
            var tipoConverter = new ValueConverter<StatusParcela, char>(
                v => (char)v,                               // enum -> char
                v => (StatusParcela)v              // char -> enum
            );

            builder.Property(p => p.Status)
                .HasConversion(tipoConverter)
                .HasColumnType("char(1)")
                .IsUnicode(false)
                .HasDefaultValue(StatusParcela.Null);           
        }
    }
}
