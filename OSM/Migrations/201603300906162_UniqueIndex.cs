namespace OSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Land", "IsoCode", unique: true, name: "Iso_Unique");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Land", "Iso_Unique");
        }
    }
}
