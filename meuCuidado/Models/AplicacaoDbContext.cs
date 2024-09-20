using System.Data.Entity;

namespace meuCuidado.Models
{
    public class AplicacaoDbContext : DbContext
    {
        public AplicacaoDbContext() : base("name=AplicacaoDbContext") { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Idoso> Idosos { get; set; }
        public DbSet<CuidadorDeIdoso> Cuidadores { get; set; }
        public DbSet<Fisioterapeuta> Fisioterapeutas { get; set; }
        public DbSet<Familiar> Familiares { get; set; }
        public DbSet<CuidadorIdoso> CuidadorIdosos { get; set; }
        public DbSet<FisioterapeutaIdoso> FisioterapeutaIdosos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuidadorIdoso>()
                .HasKey(ci => new { ci.CuidadorId, ci.IdosoId });

            modelBuilder.Entity<CuidadorIdoso>()
                .HasRequired(ci => ci.Cuidador)
                .WithMany(c => c.IdososCadastrados)
                .HasForeignKey(ci => ci.CuidadorId);

            modelBuilder.Entity<CuidadorIdoso>()
                .HasRequired(ci => ci.Idoso)
                .WithMany(i => i.Cuidadores)
                .HasForeignKey(ci => ci.IdosoId);

            modelBuilder.Entity<FisioterapeutaIdoso>()
                .HasKey(fi => new { fi.FisioterapeutaId, fi.IdosoId });

            modelBuilder.Entity<FisioterapeutaIdoso>()
                .HasRequired(fi => fi.Fisioterapeuta)
                .WithMany(f => f.IdososCadastrados)
                .HasForeignKey(fi => fi.FisioterapeutaId);

            modelBuilder.Entity<FisioterapeutaIdoso>()
                .HasRequired(fi => fi.Idoso)
                .WithMany(i => i.Fisioterapeutas)
                .HasForeignKey(fi => fi.IdosoId);
        }
    }
}