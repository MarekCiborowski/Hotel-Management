namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationTotalCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservation", "TotalCost");
        }
    }
}
