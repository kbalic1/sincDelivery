namespace sincDelivery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class korisnikID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "KorisnikTipID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "KorisnikTipID");
        }
    }
}
