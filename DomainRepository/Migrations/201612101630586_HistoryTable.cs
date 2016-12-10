namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HitAt = c.DateTime(nullable: false),
                        ShortUrlId = c.Int(nullable: false),
                        MetaData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShortUrls", t => t.ShortUrlId, cascadeDelete: true)
                .Index(t => t.ShortUrlId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestHistories", "ShortUrlId", "dbo.ShortUrls");
            DropIndex("dbo.RequestHistories", new[] { "ShortUrlId" });
            DropTable("dbo.RequestHistories");
        }
    }
}
