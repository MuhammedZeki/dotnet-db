using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_db.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityOrderv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNote",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNote",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
