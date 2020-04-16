using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionRevisedApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(nullable: true),
                    AnswerOne = table.Column<string>(nullable: true),
                    AnswerTwo = table.Column<string>(nullable: true),
                    AnswerThree = table.Column<string>(nullable: true),
                    CorrectAnswerIndex = table.Column<int>(nullable: false),
                    AnswerExplanation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
