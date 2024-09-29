using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using meuCuidado.Models;

namespace meuCuidado.Models
{
    public class MeuCuidadoDbContext : DbContext
    {
        public MeuCuidadoDbContext() : base("MeuCuidadoDbContext")
        {
        }

        public DbSet<RelacionamentoIdosoProfissional> RelacionamentosIdosoProfissional { get; set; }
        public DbSet<Lembrete> Lembretes { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<CuidadorDeIdoso> CuidadoresDeIdoso { get; set; }
        public DbSet<Fisioterapeuta> Fisioterapeutas { get; set; }
        public DbSet<Idoso> Idosos { get; set; }
        public DbSet<Tutor> Tutores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configurações para RelacionamentoIdosoProfissional
            modelBuilder.Entity<RelacionamentoIdosoProfissional>()
                .HasKey(r => r.Id); // Define a chave primária

            modelBuilder.Entity<RelacionamentoIdosoProfissional>()
                .HasOptional(r => r.Cuidador)
                .WithMany()
                .HasForeignKey(r => r.CuidadorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelacionamentoIdosoProfissional>()
                .HasOptional(r => r.Fisioterapeuta)
                .WithMany()
                .HasForeignKey(r => r.FisioterapeutaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelacionamentoIdosoProfissional>()
                .HasOptional(r => r.Idoso)
                .WithMany()
                .HasForeignKey(r => r.IdosoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelacionamentoIdosoProfissional>()
                .HasOptional(r => r.Tutor)
                .WithMany()
                .HasForeignKey(r => r.TutorId)
                .WillCascadeOnDelete(false);

            // Configurações para Lembrete
            modelBuilder.Entity<Lembrete>()
                .HasKey(l => l.Id); // Define a chave primária

            modelBuilder.Entity<Lembrete>()
                .HasOptional(l => l.Medicamento)
                .WithRequired(m => m.Lembrete)
                .WillCascadeOnDelete(true); // A exclusão em cascata pode ser mantida aqui, já que Medicamento é obrigatório para Lembrete

            // Configurações para Medicamento
            modelBuilder.Entity<Medicamento>()
                .HasKey(m => m.Id); // Define a chave primária

            modelBuilder.Entity<Medicamento>()
                .HasRequired(m => m.Lembrete)
                .WithOptional(l => l.Medicamento)
                .WillCascadeOnDelete(true); // Removido a exclusão em cascata aqui, porque já está na configuração de Lembrete

            // Configurações para Avaliacao
            modelBuilder.Entity<Avaliacao>()
                .HasKey(a => a.Id); // Define a chave primária

            modelBuilder.Entity<Avaliacao>()
                .HasRequired(a => a.RelacionamentoIdosoProfissional) // Supondo que a Avaliação é obrigatória para um Idoso
                .WithMany()
                .HasForeignKey(a => a.RelacionamentoIdosoProfissionalId) // Ajuste conforme necessário
                .WillCascadeOnDelete(false);

            // Configurações para CuidadorDeIdoso
            modelBuilder.Entity<CuidadorDeIdoso>()
                .HasKey(c => c.Id); // Define a chave primária

            // Configurações para Fisioterapeuta
            modelBuilder.Entity<Fisioterapeuta>()
                .HasKey(f => f.Id); // Define a chave primária

            // Configurações para Idoso
            modelBuilder.Entity<Idoso>()
                .HasKey(i => i.Id); // Define a chave primária

            // Configurações para Tutor
            modelBuilder.Entity<Tutor>()
                .HasKey(t => t.Id); // Define a chave primária

            // Aqui você pode adicionar mais configurações para as outras entidades, se necessário.
        }
    }
}
