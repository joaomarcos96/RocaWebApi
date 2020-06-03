using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RocaWebApi.Api.Migrations
{
    public partial class AddTrackableEntityAndContextModelConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "workers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "workers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "workers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "workers");
        }
    }
}
