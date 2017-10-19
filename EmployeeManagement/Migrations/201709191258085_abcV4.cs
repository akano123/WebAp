namespace EmployeeManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcV4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IdCard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "IdCard");
        }
    }
}
