namespace LG_10_Exercise_01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LG_10_Exercise_01.Models.GiftDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LG_10_Exercise_01.Models.GiftDbContext context)
        {
            context.GiftCategories.AddOrUpdate(m => m.strCategory, new Models.GiftCategory { strCategory = "Greeting Cards" }, new Models.GiftCategory { strCategory = "Toys" }, new Models.GiftCategory { strCategory = "Electronic Toys" }, new Models.GiftCategory { strCategory = "Perfumes" }, new Models.GiftCategory { strCategory = "Watches" }, new Models.GiftCategory { strCategory = "Clothes" }, new Models.GiftCategory { strCategory = "Photo Frames" });
          
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
