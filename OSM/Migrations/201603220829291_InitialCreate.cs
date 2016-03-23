namespace OSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 100),
                        Beschrijving = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Competities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 100),
                        Land_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lands", t => t.Land_ID)
                .Index(t => t.Land_ID);
            
            CreateTable(
                "dbo.Lands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 50),
                        Continent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Spelers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Competities", "Land_ID", "dbo.Lands");
            DropIndex("dbo.Competities", new[] { "Land_ID" });
            DropTable("dbo.Teams");
            DropTable("dbo.Spelers");
            DropTable("dbo.Managers");
            DropTable("dbo.Lands");
            DropTable("dbo.Competities");
            DropTable("dbo.Achievements");
        }
    }
}
