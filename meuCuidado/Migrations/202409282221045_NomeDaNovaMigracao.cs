namespace meuCuidado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeDaNovaMigracao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pessoas", newName: "CuidadorDeIdosoes");
            DropForeignKey("dbo.CuidadorIdosoes", "CuidadorId", "dbo.Pessoas");
            DropForeignKey("dbo.FisioterapeutaIdosoes", "FisioterapeutaId", "dbo.Pessoas");
            DropForeignKey("dbo.FisioterapeutaIdosoes", "IdosoId", "dbo.Pessoas");
            DropForeignKey("dbo.CuidadorIdosoes", "IdosoId", "dbo.Pessoas");
            DropIndex("dbo.CuidadorIdosoes", new[] { "CuidadorId" });
            DropIndex("dbo.CuidadorIdosoes", new[] { "IdosoId" });
            DropIndex("dbo.FisioterapeutaIdosoes", new[] { "FisioterapeutaId" });
            DropIndex("dbo.FisioterapeutaIdosoes", new[] { "IdosoId" });
            DropPrimaryKey("dbo.CuidadorDeIdosoes");
            CreateTable(
                "dbo.Avaliacaos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RelacionamentoIdosoProfissionalId = c.Guid(nullable: false),
                        Nota = c.Int(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RelacionamentoIdosoProfissionals", t => t.RelacionamentoIdosoProfissionalId, cascadeDelete: true)
                .Index(t => t.RelacionamentoIdosoProfissionalId);
            
            CreateTable(
                "dbo.RelacionamentoIdosoProfissionals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CuidadorId = c.Guid(),
                        FisioterapeutaId = c.Guid(),
                        IdosoId = c.Guid(),
                        TutorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fisioterapeutas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        CPF = c.String(),
                        Endereco = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(),
                        DataCadasto = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(),
                        UltimoLLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Idosoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        NecessidadesEspeciais = c.Boolean(nullable: false),
                        TutorId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        CPF = c.String(),
                        Endereco = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(),
                        DataCadasto = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(),
                        UltimoLLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tutors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Medicos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdosoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        CPF = c.String(),
                        Endereco = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(),
                        DataCadasto = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(),
                        UltimoLLogin = c.DateTime(),
                        Tutor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Idosoes", t => t.IdosoId, cascadeDelete: true)
                .ForeignKey("dbo.Tutors", t => t.Tutor_Id)
                .Index(t => t.IdosoId)
                .Index(t => t.Tutor_Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RelacaoComIdoso = c.String(nullable: false),
                        IdadeDoIdoso = c.DateTime(nullable: false),
                        IdosoId = c.Guid(nullable: false),
                        NecessidadesEspeciais = c.Boolean(nullable: false),
                        Nome = c.String(nullable: false),
                        CPF = c.String(),
                        Endereco = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(),
                        DataCadasto = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(),
                        UltimoLLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lembretes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RelacionamentoIdosoProfissionalId = c.Guid(nullable: false),
                        MedicamentoId = c.Guid(),
                        Descricao = c.String(),
                        DataHora = c.DateTime(nullable: false),
                        Repete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RelacionamentoIdosoProfissionals", t => t.RelacionamentoIdosoProfissionalId, cascadeDelete: true)
                .Index(t => t.RelacionamentoIdosoProfissionalId);
            
            CreateTable(
                "dbo.Medicamentoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Dosagem = c.String(nullable: false),
                        DataHoraPrimeiroAlerta = c.DateTime(nullable: false),
                        DataHoraSegundoAlerta = c.DateTime(),
                        FormaFarmaceutica = c.String(),
                        DuracaoEmDias = c.Int(nullable: false),
                        Observacoes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lembretes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProfissionalValidacaos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CuidadorId = c.Guid(),
                        FisioterapeutaId = c.Guid(),
                        CertificadoPM = c.String(),
                        CPF = c.String(),
                        RG = c.String(),
                        Recomendacoes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CuidadorDeIdosoes", "DataCadasto", c => c.DateTime(nullable: false));
            AddColumn("dbo.CuidadorDeIdosoes", "DataAtualizacao", c => c.DateTime());
            AddColumn("dbo.CuidadorDeIdosoes", "UltimoLLogin", c => c.DateTime());
            AlterColumn("dbo.CuidadorDeIdosoes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.CuidadorDeIdosoes", "CPF", c => c.String());
            AlterColumn("dbo.CuidadorDeIdosoes", "Senha", c => c.String());
            AddPrimaryKey("dbo.CuidadorDeIdosoes", "Id");
            DropColumn("dbo.CuidadorDeIdosoes", "RegistroProfissional");
            DropColumn("dbo.CuidadorDeIdosoes", "DataNascimento");
            DropColumn("dbo.CuidadorDeIdosoes", "RegistroProfissional1");
            DropColumn("dbo.CuidadorDeIdosoes", "RelacaoComIdoso");
            DropColumn("dbo.CuidadorDeIdosoes", "Discriminator");
            DropTable("dbo.CuidadorIdosoes");
            DropTable("dbo.FisioterapeutaIdosoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FisioterapeutaIdosoes",
                c => new
                    {
                        FisioterapeutaId = c.Int(nullable: false),
                        IdosoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FisioterapeutaId, t.IdosoId });
            
            CreateTable(
                "dbo.CuidadorIdosoes",
                c => new
                    {
                        CuidadorId = c.Int(nullable: false),
                        IdosoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CuidadorId, t.IdosoId });
            
            AddColumn("dbo.CuidadorDeIdosoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CuidadorDeIdosoes", "RelacaoComIdoso", c => c.String());
            AddColumn("dbo.CuidadorDeIdosoes", "RegistroProfissional1", c => c.String());
            AddColumn("dbo.CuidadorDeIdosoes", "DataNascimento", c => c.DateTime());
            AddColumn("dbo.CuidadorDeIdosoes", "RegistroProfissional", c => c.String());
            DropForeignKey("dbo.Lembretes", "RelacionamentoIdosoProfissionalId", "dbo.RelacionamentoIdosoProfissionals");
            DropForeignKey("dbo.Medicamentoes", "Id", "dbo.Lembretes");
            DropForeignKey("dbo.Idosoes", "Id", "dbo.Tutors");
            DropForeignKey("dbo.Medicos", "Tutor_Id", "dbo.Tutors");
            DropForeignKey("dbo.Medicos", "IdosoId", "dbo.Idosoes");
            DropForeignKey("dbo.Avaliacaos", "RelacionamentoIdosoProfissionalId", "dbo.RelacionamentoIdosoProfissionals");
            DropIndex("dbo.Medicamentoes", new[] { "Id" });
            DropIndex("dbo.Lembretes", new[] { "RelacionamentoIdosoProfissionalId" });
            DropIndex("dbo.Medicos", new[] { "Tutor_Id" });
            DropIndex("dbo.Medicos", new[] { "IdosoId" });
            DropIndex("dbo.Idosoes", new[] { "Id" });
            DropIndex("dbo.Avaliacaos", new[] { "RelacionamentoIdosoProfissionalId" });
            DropPrimaryKey("dbo.CuidadorDeIdosoes");
            AlterColumn("dbo.CuidadorDeIdosoes", "Senha", c => c.String(nullable: false));
            AlterColumn("dbo.CuidadorDeIdosoes", "CPF", c => c.String(nullable: false));
            AlterColumn("dbo.CuidadorDeIdosoes", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.CuidadorDeIdosoes", "UltimoLLogin");
            DropColumn("dbo.CuidadorDeIdosoes", "DataAtualizacao");
            DropColumn("dbo.CuidadorDeIdosoes", "DataCadasto");
            DropTable("dbo.ProfissionalValidacaos");
            DropTable("dbo.Medicamentoes");
            DropTable("dbo.Lembretes");
            DropTable("dbo.Tutors");
            DropTable("dbo.Medicos");
            DropTable("dbo.Idosoes");
            DropTable("dbo.Fisioterapeutas");
            DropTable("dbo.RelacionamentoIdosoProfissionals");
            DropTable("dbo.Avaliacaos");
            AddPrimaryKey("dbo.CuidadorDeIdosoes", "Id");
            CreateIndex("dbo.FisioterapeutaIdosoes", "IdosoId");
            CreateIndex("dbo.FisioterapeutaIdosoes", "FisioterapeutaId");
            CreateIndex("dbo.CuidadorIdosoes", "IdosoId");
            CreateIndex("dbo.CuidadorIdosoes", "CuidadorId");
            AddForeignKey("dbo.CuidadorIdosoes", "IdosoId", "dbo.Pessoas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FisioterapeutaIdosoes", "IdosoId", "dbo.Pessoas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FisioterapeutaIdosoes", "FisioterapeutaId", "dbo.Pessoas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CuidadorIdosoes", "CuidadorId", "dbo.Pessoas", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.CuidadorDeIdosoes", newName: "Pessoas");
        }
    }
}
