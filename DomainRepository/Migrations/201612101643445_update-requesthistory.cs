namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterequesthistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestHistories", "Country", c => c.String());
            AddColumn("dbo.RequestHistories", "Browser", c => c.String());
            AddColumn("dbo.RequestHistories", "Platforms", c => c.String());
            AddColumn("dbo.RequestHistories", "Website", c => c.String());
            DropColumn("dbo.RequestHistories", "MetaData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestHistories", "MetaData", c => c.String());
            DropColumn("dbo.RequestHistories", "Website");
            DropColumn("dbo.RequestHistories", "Platforms");
            DropColumn("dbo.RequestHistories", "Browser");
            DropColumn("dbo.RequestHistories", "Country");
        }
    }
}
