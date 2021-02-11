namespace MMarketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameMMarketing : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MMarketings", newName: "MarketingViewModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MarketingViewModels", newName: "MMarketings");
        }
    }
}
