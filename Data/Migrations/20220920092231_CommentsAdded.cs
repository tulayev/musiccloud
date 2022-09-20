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
                    TrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                values: new object[] { "eb19a3e3-256b-478d-9036-1a292c9a7568", 0, null, "1c5de472-fddb-4dcc-8e39-4e61d99b0f72", "Jon", "jon@test.com", false, null, false, null, "JON@TEST.COM", "JON", "AQAAAAEAACcQAAAAEJ1YkScfUoy3/1sr1E5VbRPmPgsR4gnpzU+csVs2V4wylx/DherrF7awZaRiA0GeCA==", null, false, "5E75C5E2-7F6E-4483-B7B8-518BB1F54651", false, "jon" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("379018e4-56e7-4e47-8ce9-d9ae216d1ac5"), null, "Pink Floyd", "Rock", null, "Marooned", "eb19a3e3-256b-478d-9036-1a292c9a7568" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("483ac019-7cea-431c-8fdb-c9f06546eb81"), null, "Post Malone, Ozzy Osbourne & Travi$ Scott", "Hip-Hop", null, "Take What You Want", "eb19a3e3-256b-478d-9036-1a292c9a7568" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AudioId", "Author", "Genre", "PosterId", "Title", "UserId" },
                values: new object[] { new Guid("cffd357d-fc02-4133-88e2-c5497044514d"), null, "Martin Garrix & Brooks", "ElectroHouse", null, "Byte", "eb19a3e3-256b-478d-9036-1a292c9a7568" });

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
                keyValue: new Guid("379018e4-56e7-4e47-8ce9-d9ae216d1ac5"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("483ac019-7cea-431c-8fdb-c9f06546eb81"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("cffd357d-fc02-4133-88e2-c5497044514d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb19a3e3-256b-478d-9036-1a292c9a7568");

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
