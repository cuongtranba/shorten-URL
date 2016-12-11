namespace DomainRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecolumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RequestHistories", "Website");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestHistories", "Website", c => c.String());
        }
    }
}
