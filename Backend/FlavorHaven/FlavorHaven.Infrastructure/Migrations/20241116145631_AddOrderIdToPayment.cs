using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlavorHaven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderIdToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4fb4100a-311a-4ee4-8f27-5151d3f80ab1"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("27f2618b-316c-4723-86db-d6edb3b76977"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("54fb8379-499e-42ab-bf61-66e8f44d9e18"), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("ac6af44d-a0ce-4d0c-a4f0-8c67c2cb4c18"), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$.vqV86CrwqydyHQWh0rxZeVyNIdWr0TvUuxfGelqIjnNKboXu28Oe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("54fb8379-499e-42ab-bf61-66e8f44d9e18"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac6af44d-a0ce-4d0c-a4f0-8c67c2cb4c18"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("4fb4100a-311a-4ee4-8f27-5151d3f80ab1"), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("27f2618b-316c-4723-86db-d6edb3b76977"), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$Loqc4DDy.y9PxugCz5USJewKOqN.WRXAwmHEYUV3js7NmWcEVj.EO");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
