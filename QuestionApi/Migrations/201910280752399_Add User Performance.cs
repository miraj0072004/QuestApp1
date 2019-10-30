namespace QuestionApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPerformance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPerformances",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TotalQuestions = c.Int(nullable: false),
                        CorrectAnswerCount = c.Int(nullable: false),
                        TotalGamesCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPerformances");
        }
    }
}
