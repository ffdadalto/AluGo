using AluGo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AluGo.Data
{
    public class ImovelConfiguration: IEntityTypeConfiguration<Imovel>
    {
        public void Configure(EntityTypeBuilder<Imovel> builder)
        {
            builder
                .Property(i => i.Tipo)
                .HasConversion(p => (char)p, p => (TipoImovel)(char)p);
        }
    }
}
