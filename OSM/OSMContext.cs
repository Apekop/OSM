using System.Data.Entity.ModelConfiguration.Conventions;
using OSM.Models;

namespace OSM
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class OSMContext : DbContext
    {
        // Your context has been configured to use a 'OSMContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'OSM.OSMContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OSMContext' 
        // connection string in the application configuration file.
        public OSMContext()
            : base("name=OSMContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Team> Teams { get; set; }
        public DbSet<Speler> Spelers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Competitie> Competities { get; set; }
        public DbSet<Land> Landen { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        // Overschrijf default instellingen door deze methode te overriden
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Zoals het default in meervoud zetten van tabelnamen
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // En deze moet natuurlijk blijven staan
            base.OnModelCreating(modelBuilder);
        }
    }
}