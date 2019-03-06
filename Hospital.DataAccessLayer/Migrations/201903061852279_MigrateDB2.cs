namespace Hospital.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PhoneNumber", c => c.String());
            AddColumn("dbo.Doctors", "PhoneNumber", c => c.String());
            AddColumn("dbo.Doctors", "AdditionalInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "AdditionalInformation");
            DropColumn("dbo.Doctors", "PhoneNumber");
            DropColumn("dbo.Patients", "PhoneNumber");
        }
    }
}
