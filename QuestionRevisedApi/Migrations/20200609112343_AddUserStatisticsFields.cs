using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionRevisedApi.Migrations
{
    public partial class AddUserStatisticsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerCount",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalGamesCount",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuestions",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalGamesCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalQuestions",
                table: "Users");
        }
    }
}
