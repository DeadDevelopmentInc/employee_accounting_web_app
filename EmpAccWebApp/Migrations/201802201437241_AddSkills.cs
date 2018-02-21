namespace EmpAccWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillsModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        C_Sharp = c.Int(nullable: false),
                        C_or_Cpp = c.Int(nullable: false),
                        Java = c.Int(nullable: false),
                        Python = c.Int(nullable: false),
                        SQL = c.Int(nullable: false),
                        Ruby = c.Int(nullable: false),
                        VB = c.Int(nullable: false),
                        Perl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SkillsModels");
        }
    }
}
