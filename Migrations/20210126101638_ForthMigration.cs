using Microsoft.EntityFrameworkCore.Migrations;

namespace Suggester.APIv2.Migrations
{
    public partial class ForthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Sessions",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Text",
                table: "Sessions",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
