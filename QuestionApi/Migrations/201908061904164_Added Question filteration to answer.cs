namespace QuestionApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuestionfilterationtoanswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "QuestionAnswerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "QuestionAnswerId");
        }
    }
}
