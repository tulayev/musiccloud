using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CommentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("1da56434-e486-4e1c-8f17-06b39a7a5672"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("372d5f65-77a0-433f-823c-1f2219ce750b"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("cfd431e8-42d4-4495-82f1-3376b2acb2f6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6854894-e2b4-4da0-856f-d0a75e170d5a");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: true),
                    TrackId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "ImageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d7d95cd0-d3a6-4951-8c6e-0441a71ebc9a", 0, null, "df6baeed-6d19-4ee3-b20c-ba649c902da7", "Jon", "jon@test.com", false, null, false, null, "JON@TEST.COM", "JON", "AQAAAAEAACcQAAAAEIskfR5ZQB6QsYsafuQDsyAA1UO0JGYqq97X6W5Y503+qHw1IVpCigelG7+Oa9TlPw==", null, false, "6F7697F2-D8E0-4B90-BA70-085EF99E4B96", false, "jon" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("74dbcf8d-f7d2-4d8c-900d-cd4e20eb0489"), null, "Post Malone, Ozzy Osbourne & Travi$ Scott", "Hip-Hop", null, "Take What You Want", "d7d95cd0-d3a6-4951-8c6e-0441a71ebc9a" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("8491c081-cbdd-4253-b492-0189b574b7ea"), null, "Pink Floyd", "Rock", null, "Marooned", "d7d95cd0-d3a6-4951-8c6e-0441a71ebc9a" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("b8d39ce1-57ba-4cc6-a4c5-df25abf84d9c"), null, "Martin Garrix & Brooks", "ElectroHouse", null, "Byte", "d7d95cd0-d3a6-4951-8c6e-0441a71ebc9a" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TrackId",
                table: "Comments",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("74dbcf8d-f7d2-4d8c-900d-cd4e20eb0489"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("8491c081-cbdd-4253-b492-0189b574b7ea"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("b8d39ce1-57ba-4cc6-a4c5-df25abf84d9c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7d95cd0-d3a6-4951-8c6e-0441a71ebc9a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "ImageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d6854894-e2b4-4da0-856f-d0a75e170d5a", 0, null, "73270415-8b61-43b0-824e-ee47babcda89", "Jon", "jon@test.com", false, null, false, null, "JON@TEST.COM", "JON", "AQAAAAEAACcQAAAAELKrSwxkY1dpp7/bMKepK/SWDogYaonh+dRPECcP81fE/tz0rWzzdJ4mjwlCI3Q7hQ==", null, false, "EADBFDC1-6332-46A5-B988-96C3CF9F46EB", false, "jon" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("1da56434-e486-4e1c-8f17-06b39a7a5672"), null, "Martin Garrix & Brooks", "ElectroHouse", null, "Byte", "d6854894-e2b4-4da0-856f-d0a75e170d5a" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("372d5f65-77a0-433f-823c-1f2219ce750b"), null, "Pink Floyd", "Rock", null, "Marooned", "d6854894-e2b4-4da0-856f-d0a75e170d5a" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("cfd431e8-42d4-4495-82f1-3376b2acb2f6"), null, "Post Malone, Ozzy Osbourne & Travi$ Scott", "Hip-Hop", null, "Take What You Want", "d6854894-e2b4-4da0-856f-d0a75e170d5a" });
        }
    }
}
