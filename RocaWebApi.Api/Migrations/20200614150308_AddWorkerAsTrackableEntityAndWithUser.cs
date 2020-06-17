using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RocaWebApi.Api.Migrations
{
    public partial class AddWorkerAsTrackableEntityAndWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    updated_at = table.Column<DateTimeOffset>(nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workers", x => x.id);
                    table.ForeignKey(
                        name: "fk_workers_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workers");
        }
    }
}
