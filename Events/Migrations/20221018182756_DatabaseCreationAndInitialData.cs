using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Events.Migrations
{
    public partial class DatabaseCreationAndInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Speeker = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventDateAndPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EventDateAndPlace", "Name", "Speeker" },
                values: new object[] { new Guid("b76c5f5b-4970-4f8a-94c6-c7c5266bbba5"), "description-filler", "17.09 at Main Building", "Tech. Interview", "me" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EventDateAndPlace", "Name", "Speeker" },
                values: new object[] { new Guid("248d2d82-6e92-4383-9206-fdac9876e6d2"), "filler-descr", "18.09 at BSTU", "for test", "not me" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
