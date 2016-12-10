namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShortUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserIpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserIps", t => t.UserIpId, cascadeDelete: true)
                .Index(t => t.UserIpId);
            
            CreateTable(
                "dbo.UserIps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ip = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShortUrls", "UserIpId", "dbo.UserIps");
            DropIndex("dbo.ShortUrls", new[] { "UserIpId" });
            DropTable("dbo.UserIps");
            DropTable("dbo.ShortUrls");
        }
    }
}
