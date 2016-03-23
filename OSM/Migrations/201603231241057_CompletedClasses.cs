namespace OSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedClasses : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Achievements", newName: "Achievement");
            RenameTable(name: "dbo.Competities", newName: "Competitie");
            RenameTable(name: "dbo.Lands", newName: "Land");
            RenameTable(name: "dbo.Managers", newName: "Manager");
            RenameTable(name: "dbo.Spelers", newName: "Speler");
            RenameTable(name: "dbo.Teams", newName: "Team");
            CreateTable(
                "dbo.ManagerAchievement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Achievement_ID = c.Int(),
                        Manager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Achievement", t => t.Achievement_ID)
                .ForeignKey("dbo.Manager", t => t.Manager_ID)
                .Index(t => t.Achievement_ID)
                .Index(t => t.Manager_ID);
            
            CreateTable(
                "dbo.LandHistorie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompetitieGewonnen = c.Int(nullable: false),
                        BekerGewonnen = c.Int(nullable: false),
                        DoelstellingBehaald = c.Int(nullable: false),
                        Land_ID = c.Int(),
                        Manager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Land", t => t.Land_ID)
                .ForeignKey("dbo.Manager", t => t.Manager_ID)
                .Index(t => t.Land_ID)
                .Index(t => t.Manager_ID);
            
            CreateTable(
                "dbo.ManagerVriend",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Manager_ID = c.Int(),
                        Vriend_ID = c.Int(),
                        Manager_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manager", t => t.Manager_ID)
                .ForeignKey("dbo.Manager", t => t.Vriend_ID)
                .ForeignKey("dbo.Manager", t => t.Manager_ID1)
                .Index(t => t.Manager_ID)
                .Index(t => t.Vriend_ID)
                .Index(t => t.Manager_ID1);
            
            CreateTable(
                "dbo.Historie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        Team1_ID = c.Int(),
                        Team2_ID = c.Int(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Team", t => t.Team1_ID)
                .ForeignKey("dbo.Team", t => t.Team2_ID)
                .ForeignKey("dbo.Team", t => t.Team_ID)
                .Index(t => t.Team1_ID)
                .Index(t => t.Team2_ID)
                .Index(t => t.Team_ID);
            
            AddColumn("dbo.Land", "Beschikbaar", c => c.Boolean(nullable: false));
            AddColumn("dbo.Speler", "Team_ID", c => c.Int());
            AddColumn("dbo.Team", "Doelstelling", c => c.Int(nullable: false));
            AddColumn("dbo.Team", "Competitie_ID", c => c.Int());
            AddColumn("dbo.Team", "Manager_ID", c => c.Int());
            CreateIndex("dbo.Team", "Competitie_ID");
            CreateIndex("dbo.Team", "Manager_ID");
            CreateIndex("dbo.Speler", "Team_ID");
            AddForeignKey("dbo.Team", "Competitie_ID", "dbo.Competitie", "ID");
            AddForeignKey("dbo.Team", "Manager_ID", "dbo.Manager", "ID");
            AddForeignKey("dbo.Speler", "Team_ID", "dbo.Team", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Historie", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.Historie", "Team2_ID", "dbo.Team");
            DropForeignKey("dbo.Historie", "Team1_ID", "dbo.Team");
            DropForeignKey("dbo.Speler", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.Team", "Manager_ID", "dbo.Manager");
            DropForeignKey("dbo.ManagerVriend", "Manager_ID1", "dbo.Manager");
            DropForeignKey("dbo.ManagerVriend", "Vriend_ID", "dbo.Manager");
            DropForeignKey("dbo.ManagerVriend", "Manager_ID", "dbo.Manager");
            DropForeignKey("dbo.LandHistorie", "Manager_ID", "dbo.Manager");
            DropForeignKey("dbo.LandHistorie", "Land_ID", "dbo.Land");
            DropForeignKey("dbo.ManagerAchievement", "Manager_ID", "dbo.Manager");
            DropForeignKey("dbo.ManagerAchievement", "Achievement_ID", "dbo.Achievement");
            DropForeignKey("dbo.Team", "Competitie_ID", "dbo.Competitie");
            DropIndex("dbo.Historie", new[] { "Team_ID" });
            DropIndex("dbo.Historie", new[] { "Team2_ID" });
            DropIndex("dbo.Historie", new[] { "Team1_ID" });
            DropIndex("dbo.Speler", new[] { "Team_ID" });
            DropIndex("dbo.ManagerVriend", new[] { "Manager_ID1" });
            DropIndex("dbo.ManagerVriend", new[] { "Vriend_ID" });
            DropIndex("dbo.ManagerVriend", new[] { "Manager_ID" });
            DropIndex("dbo.LandHistorie", new[] { "Manager_ID" });
            DropIndex("dbo.LandHistorie", new[] { "Land_ID" });
            DropIndex("dbo.ManagerAchievement", new[] { "Manager_ID" });
            DropIndex("dbo.ManagerAchievement", new[] { "Achievement_ID" });
            DropIndex("dbo.Team", new[] { "Manager_ID" });
            DropIndex("dbo.Team", new[] { "Competitie_ID" });
            DropColumn("dbo.Team", "Manager_ID");
            DropColumn("dbo.Team", "Competitie_ID");
            DropColumn("dbo.Team", "Doelstelling");
            DropColumn("dbo.Speler", "Team_ID");
            DropColumn("dbo.Land", "Beschikbaar");
            DropTable("dbo.Historie");
            DropTable("dbo.ManagerVriend");
            DropTable("dbo.LandHistorie");
            DropTable("dbo.ManagerAchievement");
            RenameTable(name: "dbo.Team", newName: "Teams");
            RenameTable(name: "dbo.Speler", newName: "Spelers");
            RenameTable(name: "dbo.Manager", newName: "Managers");
            RenameTable(name: "dbo.Land", newName: "Lands");
            RenameTable(name: "dbo.Competitie", newName: "Competities");
            RenameTable(name: "dbo.Achievement", newName: "Achievements");
        }
    }
}
