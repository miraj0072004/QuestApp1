namespace QuestionApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangestoAutoIncrementation1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropPrimaryKey("dbo.Answers");
            DropPrimaryKey("dbo.Questions");
            AlterColumn("dbo.Answers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Questions", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Answers", "Id");
            AddPrimaryKey("dbo.Questions", "Id");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropPrimaryKey("dbo.Questions");
            DropPrimaryKey("dbo.Answers");
            AlterColumn("dbo.Questions", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Questions", "Id");
            AddPrimaryKey("dbo.Answers", "Id");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
