using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanChatApp.Migrations
{
    /// <inheritdoc />
    public partial class ChatModeWithId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssignedUserId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Projects_ProjectId",
                table: "TaskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "User",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_AssignedUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssignedUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectMembers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMembers", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectMembers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ApplicationUserId",
                table: "Projects",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ProjectId",
                table: "ChatMessages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ApplicationUserId",
                table: "ProjectMembers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_UserId",
                table: "ProjectMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Projects_ProjectId",
                table: "ChatMessages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ApplicationUserId",
                table: "Projects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedUserId",
                table: "Tasks",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Projects_ProjectId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ApplicationUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedUserId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ApplicationUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ProjectId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TaskItems");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "TaskItems",
                newName: "IX_TaskItems_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignedUserId",
                table: "TaskItems",
                newName: "IX_TaskItems_AssignedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssignedUserId",
                table: "TaskItems",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Projects_ProjectId",
                table: "TaskItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
