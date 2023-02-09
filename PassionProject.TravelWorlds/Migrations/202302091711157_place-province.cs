namespace PassionProject.TravelWorlds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placeprovince : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "ProvinceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Places", "ProvinceID");
            AddForeignKey("dbo.Places", "ProvinceID", "dbo.Provinces", "ProvinceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Places", "ProvinceID", "dbo.Provinces");
            DropIndex("dbo.Places", new[] { "ProvinceID" });
            DropColumn("dbo.Places", "ProvinceID");
        }
    }
}
