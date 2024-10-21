namespace meuCuidado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.meuCuidado_CuidadorDeIdoso", "Genero", c => c.String());
            AddColumn("dbo.meuCuidado_Fisioterapeuta", "Genero", c => c.String());
            AddColumn("dbo.meuCuidado_Idoso", "Genero", c => c.String());
            AddColumn("dbo.meuCuidado_Medico", "Genero", c => c.String());
            AddColumn("dbo.meuCuidado_Tutor", "Genero", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.meuCuidado_Tutor", "Genero");
            DropColumn("dbo.meuCuidado_Medico", "Genero");
            DropColumn("dbo.meuCuidado_Idoso", "Genero");
            DropColumn("dbo.meuCuidado_Fisioterapeuta", "Genero");
            DropColumn("dbo.meuCuidado_CuidadorDeIdoso", "Genero");
        }
    }
}
