namespace EmployeeManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ExpiryDate");
        }
    }
}
