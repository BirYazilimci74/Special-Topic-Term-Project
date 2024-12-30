namespace DBMSCourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NOCAremove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sections", "NumberOfCorrectAnswers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "NumberOfCorrectAnswers", c => c.Int(nullable: false));
        }
    }
}
