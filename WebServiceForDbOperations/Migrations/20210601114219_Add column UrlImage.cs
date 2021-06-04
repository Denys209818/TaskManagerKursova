using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServiceForDbOperations.Migrations
{
    public partial class AddcolumnUrlImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "tblBoards",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "tblBoards");
        }
    }
}
