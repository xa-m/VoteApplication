using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoteApplicaiton.Database.Migrations
{
    public partial class AddedDateSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Votes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Votes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "VoteId",
                table: "SingleVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VoteId",
                table: "SingleVotes");
        }
    }
}
