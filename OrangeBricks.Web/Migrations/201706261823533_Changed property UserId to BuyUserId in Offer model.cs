namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedpropertyUserIdtoBuyUserIdinOffermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "BuyerUserId", c => c.String());
            DropColumn("dbo.Offers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offers", "UserId", c => c.String());
            DropColumn("dbo.Offers", "BuyerUserId");
        }
    }
}
