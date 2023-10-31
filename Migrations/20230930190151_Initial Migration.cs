using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Techblog.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "TechPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlHandle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagTechPost",
                columns: table => new
                {
                    TagsTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TechPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTechPost", x => new { x.TagsTagId, x.TechPostsId });
                    table.ForeignKey(
                        name: "FK_TagTechPost_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTechPost_TechPosts_TechPostsId",
                        column: x => x.TechPostsId,
                        principalTable: "TechPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTechPost_TechPostsId",
                table: "TagTechPost",
                column: "TechPostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTechPost");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TechPosts");
        }
    }
}
