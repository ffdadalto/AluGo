using AluGo.Data.Configurations;
using AluGo.Domain;
using Microsoft.EntityFrameworkCore;

namespace AluGo.Data
{
    public class AluGoDbContext: DbContext
    {
        public DbSet<Imovel> Imoveis => Set<Imovel>();
        public DbSet<Locatario> Locatarios => Set<Locatario>();
        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<Parcela> Parcelas => Set<Parcela>();
        public DbSet<Recebimento> Recebimentos => Set<Recebimento>();
        public DbSet<Recibo> Recibos => Set<Recibo>();
        public DbSet<Reajuste> Reajustes => Set<Reajuste>();


        public AluGoDbContext(DbContextOptions<AluGoDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new ImovelConfiguration());
            mb.ApplyConfiguration(new ParcelaConfiguration());
            mb.ApplyConfiguration(new LocatarioConfiguration());

            mb.Entity<Parcela>()
            .HasIndex(p => new { p.ContratoId, p.Competencia }).IsUnique();

            mb.Entity<Parcela>()
            .Property(p => p.Status).HasConversion<byte>();

            mb.Entity<Recibo>()
            .HasIndex(r => r.ParcelaId).IsUnique();

            mb.Entity<Contrato>()
            .HasOne(c => c.Imovel).WithMany(i => i.Contratos).HasForeignKey(c => c.ImovelId);
            mb.Entity<Contrato>()
            .HasOne(c => c.Locatario).WithMany(l => l.Contratos).HasForeignKey(c => c.LocatarioId);


            base.OnModelCreating(mb);
        }
    }
}
