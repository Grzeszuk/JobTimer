namespace JobTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TaskModels", newName: "TaskModelLocals");
            AlterColumn("dbo.TaskModelLocals", "StartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TaskModelLocals", "EndTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskModelLocals", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TaskModelLocals", "StartTime", c => c.DateTime(nullable: false));
            RenameTable(name: "dbo.TaskModelLocals", newName: "TaskModels");
        }
    }
}
