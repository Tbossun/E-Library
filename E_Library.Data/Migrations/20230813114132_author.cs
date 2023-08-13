using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Library.Data.Migrations
{
    public partial class author : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1Id",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0492ac4f-d66c-4adb-98aa-b76e94f69190", "2c1d9865-9c04-492b-9c7a-69b89cd671f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2Id",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2599c5e3-58d7-4c76-ae3f-44a9f52886f4", "6b4d2f76-8a7c-454e-bbbf-3c345860ed3f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1Id",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "daff7b1b-03de-49d9-a485-42226fff83b3", "11f6ffd7-4e74-4e64-bac3-7d522fd7438e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2Id",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2294107a-e409-4157-afb2-5e0b2da9a6be", "7503400d-c8f1-4d16-bcf8-bf66e1c141d3" });
        }
    }
}
