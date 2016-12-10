namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchorturlcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShortUrls", "ShortUrlString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShortUrls", "ShortUrlString");
        }
    }
}
