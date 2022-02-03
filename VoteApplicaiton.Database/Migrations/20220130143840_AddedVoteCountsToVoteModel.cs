using Microsoft.EntityFrameworkCore.Migrations;

namespace VoteApplicaiton.Database.Migrations
{
    public partial class AddedVoteCountsToVoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteSelect1Count",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoteSelect2Count",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteSelect1Count",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VoteSelect2Count",
                table: "Votes");
        }
    }
}
