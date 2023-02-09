namespace PassionProject.TravelWorlds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class provincecountries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Provinces", "CountryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Provinces", "CountryID");
            AddForeignKey("dbo.Provinces", "CountryID", "dbo.Countries", "CountryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provinces", "CountryID", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "CountryID" });
            DropColumn("dbo.Provinces", "CountryID");
        }
    }
}
