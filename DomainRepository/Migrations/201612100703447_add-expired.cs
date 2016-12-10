namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addexpired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserIps", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserIps", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserIps", "IsExpired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserIps", "IsExpired");
            DropColumn("dbo.UserIps", "IsDeleted");
            DropColumn("dbo.UserIps", "CreatedAt");
        }
    }
}
