using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_db.Migrations
{
    /// <inheritdoc />
    public partial class ProductUpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Img", "IsHome" },
                values: new object[] { "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "1.jpeg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img", "IsHome" },
                values: new object[] { "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "2.jpeg", false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Img", "IsHome" },
                values: new object[] { "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "3.jpeg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Img", "IsHome" },
                values: new object[] { "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "4.jpeg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "IsHome" },
                values: new object[] { "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "5.jpeg", true });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Img", "IsActive", "IsHome", "Name", "Price" },
                values: new object[,]
                {
                    { 6, "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "6.jpeg", true, false, "Iphone 17", 60000.0 },
                    { 7, "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "7.jpeg", true, false, "Iphone 17", 70000.0 },
                    { 8, "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae ullam dicta sapiente recusandae? Accusamus, suscipit consequuntur doloribus magnam corrupti sed obcaecati quas vel ipsam eos exercitationem modi deserunt alias voluptas?", "8.jpeg", true, false, "Iphone 17", 80000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Products");
        }
    }
}
