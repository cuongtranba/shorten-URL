namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableuserurl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShortUrls", "UserIpId", "dbo.UserIps");
            DropIndex("dbo.ShortUrls", new[] { "UserIpId" });
            CreateTable(
                "dbo.UserUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ShortUrls", "UserUrlId", c => c.Int());
            AlterColumn("dbo.ShortUrls", "UserIpId", c => c.Int());
            CreateIndex("dbo.ShortUrls", "UserIpId");
            CreateIndex("dbo.ShortUrls", "UserUrlId");
            AddForeignKey("dbo.ShortUrls", "UserUrlId", "dbo.UserUrls", "Id");
            AddForeignKey("dbo.ShortUrls", "UserIpId", "dbo.UserIps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShortUrls", "UserIpId", "dbo.UserIps");
            DropForeignKey("dbo.ShortUrls", "UserUrlId", "dbo.UserUrls");
            DropIndex("dbo.ShortUrls", new[] { "UserUrlId" });
            DropIndex("dbo.ShortUrls", new[] { "UserIpId" });
            AlterColumn("dbo.ShortUrls", "UserIpId", c => c.Int(nullable: false));
            DropColumn("dbo.ShortUrls", "UserUrlId");
            DropTable("dbo.UserUrls");
            CreateIndex("dbo.ShortUrls", "UserIpId");
            AddForeignKey("dbo.ShortUrls", "UserIpId", "dbo.UserIps", "Id", cascadeDelete: true);
        }
    }
}
