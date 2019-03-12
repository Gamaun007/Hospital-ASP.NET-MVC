namespace Hospital.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalCardPages", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.MedicalCardPages", new[] { "Patient_Id" });
            DropColumn("dbo.MedicalCardPages", "Patient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalCardPages", "Patient_Id", c => c.Int());
            CreateIndex("dbo.MedicalCardPages", "Patient_Id");
            AddForeignKey("dbo.MedicalCardPages", "Patient_Id", "dbo.Patients", "Id");
        }
    }
}
