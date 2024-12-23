namespace DBMSCourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverallDüzeltme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OverallReports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        QuizScore = c.Decimal(precision: 18, scale: 2),
                        KnowledgeCheckScore = c.Decimal(precision: 18, scale: 2),
                        SectionOverallScore = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        SectionName = c.String(),
                        DetailedInfo = c.String(),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        QuestionText = c.String(),
                        AnswerA = c.String(),
                        AnswerB = c.String(),
                        AnswerC = c.String(),
                        AnswerD = c.String(),
                        CorrectAnswer = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.OverallReports", "SectionId", "dbo.Sections");
            DropIndex("dbo.Quizs", new[] { "SectionId" });
            DropIndex("dbo.OverallReports", new[] { "SectionId" });
            DropTable("dbo.Quizs");
            DropTable("dbo.Sections");
            DropTable("dbo.OverallReports");
        }
    }
}
