namespace EmpAccWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbOfDep : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Branches", "IsHead");
            DropColumn("dbo.Branches", "HeadFullName");
            DropColumn("dbo.Branches", "DprtmntName");
            DropColumn("dbo.Departments", "IsHead");
            DropColumn("dbo.Departments", "HeadFullName");
            DropColumn("dbo.Sectors", "IsHead");
            DropColumn("dbo.Sectors", "HeadFullName");
            DropColumn("dbo.Sectors", "BrnchName");
            DropColumn("dbo.Sectors", "DprtmntId");
            DropColumn("dbo.Sectors", "DprtmntName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sectors", "DprtmntName", c => c.String());
            AddColumn("dbo.Sectors", "DprtmntId", c => c.String());
            AddColumn("dbo.Sectors", "BrnchName", c => c.String());
            AddColumn("dbo.Sectors", "HeadFullName", c => c.String());
            AddColumn("dbo.Sectors", "IsHead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "HeadFullName", c => c.String());
            AddColumn("dbo.Departments", "IsHead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Branches", "DprtmntName", c => c.String());
            AddColumn("dbo.Branches", "HeadFullName", c => c.String());
            AddColumn("dbo.Branches", "IsHead", c => c.Boolean(nullable: false));
        }
    }
}
