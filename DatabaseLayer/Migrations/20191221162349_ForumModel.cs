using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DatabaseLayer.Migrations
{
    public partial class ForumModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumCategories",
                columns: table => new
                {
                    ForumCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCategories", x => x.ForumCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ForumCategoryId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Locked = table.Column<bool>(nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Content = table.Column<string>(nullable: false),
                    LastPost = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topics_ForumCategories_ForumCategoryId",
                        column: x => x.ForumCategoryId,
                        principalTable: "ForumCategories",
                        principalColumn: "ForumCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topics_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TopicId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Edited = table.Column<bool>(nullable: false, defaultValue: false),
                    Content = table.Column<string>(nullable: false),
                    BestAnswer = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionPosts",
                columns: table => new
                {
                    DiscussionPostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<bool>(nullable: false, defaultValue: false),
                    Content = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionPosts", x => x.DiscussionPostId);
                    table.ForeignKey(
                        name: "FK_DiscussionPosts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscussionPosts_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicRatings",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicRatings", x => new { x.StudentId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_TopicRatings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicRatings_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerRatings",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerRatings", x => new { x.AnswerId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_AnswerRatings_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerRatings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<bool>(nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerRatings_StudentId",
                table: "AnswerRatings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_StudentId",
                table: "Answers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TopicId",
                table: "Answers",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnswerId",
                table: "Comments",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentId",
                table: "Comments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionPosts_StudentId",
                table: "DiscussionPosts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionPosts_TopicId",
                table: "DiscussionPosts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumCategories_Order",
                table: "ForumCategories",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicRatings_TopicId",
                table: "TopicRatings",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ForumCategoryId",
                table: "Topics",
                column: "ForumCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_StudentId",
                table: "Topics",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerRatings");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DiscussionPosts");

            migrationBuilder.DropTable(
                name: "TopicRatings");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "ForumCategories");
        }
    }
}
