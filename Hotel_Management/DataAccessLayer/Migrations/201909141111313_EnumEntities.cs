namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            AddColumn("dbo.Reservation", "StatusId", c => c.Int(nullable: false));
            AddColumn("dbo.User", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservation", "StatusId");
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.Reservation", "StatusId", "dbo.Status", "StatusId", cascadeDelete: true);
            AddForeignKey("dbo.User", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            DropColumn("dbo.Reservation", "Status");
            DropColumn("dbo.User", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Role", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.User", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Reservation", "StatusId", "dbo.Status");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Reservation", new[] { "StatusId" });
            DropColumn("dbo.User", "RoleId");
            DropColumn("dbo.Reservation", "StatusId");
            DropTable("dbo.Roles");
            DropTable("dbo.Status");
        }
    }
}
