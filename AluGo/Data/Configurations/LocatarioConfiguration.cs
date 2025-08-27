using AluGo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AluGo.Data.Configurations
{
    public class LocatarioConfiguration : IEntityTypeConfiguration<Locatario>
    {
        public void Configure(EntityTypeBuilder<Locatario> builder)
        {
            builder
                .Property(i => i.Tipo)
                .HasConversion(p => (char)p, p => (TipoPessoa)p);
        }
    }
}
