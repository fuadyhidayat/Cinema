using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic.Services.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _002_MembuatTableDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileCode = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
