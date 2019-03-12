namespace Hospital.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicalCards", "CreatingDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.MedicalCardPages", "NotationTime", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicalCardPages", "NotationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MedicalCards", "CreatingDate", c => c.DateTime(nullable: false));
        }
    }
}
