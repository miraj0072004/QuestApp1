using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionRevisedApi.Migrations
{
    public partial class QuestionCategoryAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionCategory",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCategory",
                table: "Questions");
        }
    }
}
