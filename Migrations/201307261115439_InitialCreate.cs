namespace LG_10_Exercise_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Gift_Name = c.String(),
                        Details = c.String(),
                        Price = c.Int(nullable: false),
                        GiftCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GiftCategories", t => t.GiftCategoryID, cascadeDelete: true)
                .Index(t => t.GiftCategoryID);
            
            CreateTable(
                "dbo.GiftCategories",
                c => new
                    {
                        GiftCategoryID = c.Int(nullable: false, identity: true),
                        strCategory = c.String(),
                    })
                .PrimaryKey(t => t.GiftCategoryID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Gifts", new[] { "GiftCategoryID" });
            DropForeignKey("dbo.Gifts", "GiftCategoryID", "dbo.GiftCategories");
            DropTable("dbo.GiftCategories");
            DropTable("dbo.Gifts");
        }
    }
}
