namespace meuCuidado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false),
                    CPF = c.String(nullable: false),
                    Endereco = c.String(nullable: false),
                    Telefone = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Senha = c.String(nullable: false),
                    RegistroProfissional = c.String(),
                    DataNascimento = c.DateTime(),
                    RegistroProfissional1 = c.String(),
                    RelacaoComIdoso = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CuidadorIdosoes",
                c => new
                {
                    CuidadorId = c.Int(nullable: false),
                    IdosoId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CuidadorId, t.IdosoId })
                .ForeignKey("dbo.Pessoas", t => t.CuidadorId, cascadeDelete: false) // Alterado para false
                .ForeignKey("dbo.Pessoas", t => t.IdosoId, cascadeDelete: false)   // Alterado para false
                .Index(t => t.CuidadorId)
                .Index(t => t.IdosoId);

            CreateTable(
                "dbo.FisioterapeutaIdosoes",
                c => new
                {
                    FisioterapeutaId = c.Int(nullable: false),
                    IdosoId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.FisioterapeutaId, t.IdosoId })
                .ForeignKey("dbo.Pessoas", t => t.FisioterapeutaId, cascadeDelete: false) // Alterado para false
                .ForeignKey("dbo.Pessoas", t => t.IdosoId, cascadeDelete: false)       // Alterado para false
                .Index(t => t.FisioterapeutaId)
                .Index(t => t.IdosoId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.CuidadorIdosoes", "IdosoId", "dbo.Pessoas");
            DropForeignKey("dbo.FisioterapeutaIdosoes", "IdosoId", "dbo.Pessoas");
            DropForeignKey("dbo.FisioterapeutaIdosoes", "FisioterapeutaId", "dbo.Pessoas");
            DropForeignKey("dbo.CuidadorIdosoes", "CuidadorId", "dbo.Pessoas");
            DropIndex("dbo.FisioterapeutaIdosoes", new[] { "IdosoId" });
            DropIndex("dbo.FisioterapeutaIdosoes", new[] { "FisioterapeutaId" });
            DropIndex("dbo.CuidadorIdosoes", new[] { "IdosoId" });
            DropIndex("dbo.CuidadorIdosoes", new[] { "CuidadorId" });
            DropTable("dbo.FisioterapeutaIdosoes");
            DropTable("dbo.CuidadorIdosoes");
            DropTable("dbo.Pessoas");
        }
    }
}
