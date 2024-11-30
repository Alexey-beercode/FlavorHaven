using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlavorHaven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_categories_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("28ab13cb-c0d4-458f-8e04-6483bc3a9899"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("5415a515-ecd7-4f38-8499-1ca7f0729e10"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("58eb57d0-4030-4d8c-94e5-220708af6236"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("6a312435-57cf-46f8-b329-ea9be8f04351"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8f4d6034-ece7-42af-bc07-c01fd1f14143"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e14e0f0d-03ae-4fdb-b53d-f649c215542f"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("ffefb033-b691-40d6-8571-eef74d996894"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7648aed1-b4bd-4836-9b79-7a597fdb8cc2"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcd0ce9d-71ba-4e0f-b229-317b0498ae74"));

            migrationBuilder.InsertData(
                table: "DishCategories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("161e8fbe-ee11-415d-96b7-5d9089922164"), false, "Корейская кухня" },
                    { new Guid("1ec3e532-c811-4ced-bd25-ff16e09a99c4"), false, "Русская кухня" },
                    { new Guid("25069d7e-7986-45fe-ab91-325d67d7fe41"), false, "Вьетнамская кухня" },
                    { new Guid("2c6706f0-601a-488a-a905-08e96647eda9"), false, "Средиземноморская кухня" },
                    { new Guid("70453c1c-7723-4caa-93b6-7457ddd03d7a"), false, "Японская кухня" },
                    { new Guid("720ca151-d4d7-42f5-9e0d-3de725053d98"), false, "Китайская кухня" },
                    { new Guid("77d8e241-21a6-416e-803d-f612dcbcd6f1"), false, "Американская кухня" },
                    { new Guid("aeee7525-9d70-43c2-8f16-74298924edf7"), false, "Белорусская кухня" },
                    { new Guid("bc53d280-fbd1-4e0f-9543-c4c0137d1ac0"), false, "Мексиканская кухня" },
                    { new Guid("bc8d82f4-a7ef-4f3f-aea2-80dbdf87696f"), false, "Грузинская кухня" },
                    { new Guid("c9eeb335-395c-43ed-af33-57a47667c6e9"), false, "Французская кухня" },
                    { new Guid("d967c534-637e-4dd8-af4c-ebe38fe72ea8"), false, "Испанская кухня" },
                    { new Guid("e41ced16-057c-464d-8c56-5ebd1ae9dec3"), false, "Индийская кухня" },
                    { new Guid("e5c45cbb-d201-4841-ace2-2128b8c5cbb0"), false, "Итальянская кухня" },
                    { new Guid("e7a04a14-687b-4b73-937c-b87a767cfe4f"), false, "Тайская кухня" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("099ed6ad-4323-40e8-ab4f-5af05032003e"), false, "Cancelled" },
                    { new Guid("2314de32-2e86-4869-8008-8eb828cf7887"), false, "Created" },
                    { new Guid("5cd95ec8-2ea3-4550-be1f-b1e4aa19db2f"), false, "Ready" },
                    { new Guid("7b05f88b-5cd4-4ed8-aa22-52c4a08be24a"), false, "Cooking" },
                    { new Guid("7fbb8085-62f3-490d-8e7e-dbd437aae744"), false, "Processing" },
                    { new Guid("81881342-49e3-4f7c-8eeb-1042b1a7aa67"), false, "Delivering" },
                    { new Guid("eae2cd85-da94-4253-8570-3f69eecc1e56"), false, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("f0edc8bb-415b-4fa6-bd39-4ea1b88193f1"), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("5bdd7d3f-2830-496e-9279-22d10991135b"), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$VeYQU5caDBo7XBTCm2t/EunaNEeUNrXzT4.ETclhhubiPFq4izKE6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("161e8fbe-ee11-415d-96b7-5d9089922164"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("1ec3e532-c811-4ced-bd25-ff16e09a99c4"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("25069d7e-7986-45fe-ab91-325d67d7fe41"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("2c6706f0-601a-488a-a905-08e96647eda9"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("70453c1c-7723-4caa-93b6-7457ddd03d7a"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("720ca151-d4d7-42f5-9e0d-3de725053d98"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("77d8e241-21a6-416e-803d-f612dcbcd6f1"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("aeee7525-9d70-43c2-8f16-74298924edf7"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("bc53d280-fbd1-4e0f-9543-c4c0137d1ac0"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("bc8d82f4-a7ef-4f3f-aea2-80dbdf87696f"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("c9eeb335-395c-43ed-af33-57a47667c6e9"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("d967c534-637e-4dd8-af4c-ebe38fe72ea8"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("e41ced16-057c-464d-8c56-5ebd1ae9dec3"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("e5c45cbb-d201-4841-ace2-2128b8c5cbb0"));

            migrationBuilder.DeleteData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: new Guid("e7a04a14-687b-4b73-937c-b87a767cfe4f"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("099ed6ad-4323-40e8-ab4f-5af05032003e"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2314de32-2e86-4869-8008-8eb828cf7887"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("5cd95ec8-2ea3-4550-be1f-b1e4aa19db2f"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("7b05f88b-5cd4-4ed8-aa22-52c4a08be24a"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("7fbb8085-62f3-490d-8e7e-dbd437aae744"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("81881342-49e3-4f7c-8eeb-1042b1a7aa67"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("eae2cd85-da94-4253-8570-3f69eecc1e56"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f0edc8bb-415b-4fa6-bd39-4ea1b88193f1"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("5bdd7d3f-2830-496e-9279-22d10991135b"));

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("28ab13cb-c0d4-458f-8e04-6483bc3a9899"), false, "Ready" },
                    { new Guid("5415a515-ecd7-4f38-8499-1ca7f0729e10"), false, "Cancelled" },
                    { new Guid("58eb57d0-4030-4d8c-94e5-220708af6236"), false, "Processing" },
                    { new Guid("6a312435-57cf-46f8-b329-ea9be8f04351"), false, "Created" },
                    { new Guid("8f4d6034-ece7-42af-bc07-c01fd1f14143"), false, "Cooking" },
                    { new Guid("e14e0f0d-03ae-4fdb-b53d-f649c215542f"), false, "Completed" },
                    { new Guid("ffefb033-b691-40d6-8571-eef74d996894"), false, "Delivering" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("7648aed1-b4bd-4836-9b79-7a597fdb8cc2"), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("dcd0ce9d-71ba-4e0f-b229-317b0498ae74"), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$LQ9qLAzQcDXXXaC7pNVJ3.41k/JlwERIy0RNxBpPSS1aUUVqKPEB2");
        }
    }
}
