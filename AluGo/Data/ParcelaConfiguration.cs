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
            builder
                .Property(i => i.Status)
                .HasConversion(p => (char)p, p => (StatusParcela)(char)p);
        }
    }
}
