namespace meuCuidado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoDeDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.meuCuidado_Medicamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentificadorUnico = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Dosagem = c.String(nullable: false),
                        DataHoraPrimeiroAlerta = c.DateTime(nullable: false),
                        DataHoraSegundoAlerta = c.DateTime(),
                        FormaFarmaceutica = c.String(),
                        DuracaoEmDias = c.Int(nullable: false),
                        Observacoes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.meuCuidado_Lembrete", "MedicamentoId", c => c.Int());
            AddColumn("dbo.meuCuidado_Lembrete", "Medicamento_Id", c => c.Int());
            CreateIndex("dbo.meuCuidado_Lembrete", "Medicamento_Id");
            AddForeignKey("dbo.meuCuidado_Lembrete", "Medicamento_Id", "dbo.meuCuidado_Medicamento", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.meuCuidado_Lembrete", "Medicamento_Id", "dbo.meuCuidado_Medicamento");
            DropIndex("dbo.meuCuidado_Lembrete", new[] { "Medicamento_Id" });
            DropColumn("dbo.meuCuidado_Lembrete", "Medicamento_Id");
            DropColumn("dbo.meuCuidado_Lembrete", "MedicamentoId");
            DropTable("dbo.meuCuidado_Medicamento");
        }
    }
}
