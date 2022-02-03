using Microsoft.EntityFrameworkCore.Migrations;

namespace VoteApplicaiton.Database.Migrations
{
    public partial class AddedSlugFieldToVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Votes");
        }
    }
}
