namespace OSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isocodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lands", "IsoCode", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lands", "IsoCode");
        }
    }
}
