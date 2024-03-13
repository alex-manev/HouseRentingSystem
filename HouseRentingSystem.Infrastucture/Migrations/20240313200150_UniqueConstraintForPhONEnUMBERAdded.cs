using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastucture.Migrations
{
    public partial class UniqueConstraintForPhONEnUMBERAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8808aabc-a1c3-41c9-8d47-ed0e357b2672", "AQAAAAEAACcQAAAAELrrua1fWYq6YqnC4moI1HvLyiWOEmrQWXPUzfZrNrUMxrbmobZTHPN5c9lpQgeLiQ==", "2c256a7f-8920-42f2-bd53-7d2b19c8407d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "112ba114-b886-4c74-9362-f2e342367ebf", "AQAAAAEAACcQAAAAEJg2C7b1HvSMdwhvrZ6ISMUO/EG+tw996JRoUrHwtDQh7v9pN65tdicsf15jMhqCHw==", "a148e7a9-f37e-49ca-812e-96d27f791cd2" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52c05166-e3e2-409a-bd30-f3fa15795644", "AQAAAAEAACcQAAAAEBxTKSEWI7Hy3lg59uyGsONw1TLC7QG5JRxNaZpYyIhdm4f2jztx1rDkdpHmXIB0Gw==", "5ea567b1-3e12-44c5-b4bc-d397e54375f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4eeba408-ad39-4205-bb71-7b4865c873aa", "AQAAAAEAACcQAAAAEAalz1HnUOWkVdWGDPx31rkz5U6UdLcakBqLR9me63UxGZjZVx9pUxbJH+6GYCpbTA==", "a2bcbc67-8b5c-491c-ba98-7629fcd3078a" });
        }
    }
}
