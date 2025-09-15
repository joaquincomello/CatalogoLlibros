using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatalogoLlibros.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    anioPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_autorId",
                        column: x => x.autorId,
                        principalTable: "Autores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "George Orwell" },
                    { 2, "Ray Bradbury" },
                    { 3, "Aldous Huxley" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "id", "UrlImagen", "anioPublicacion", "autorId", "titulo" },
                values: new object[,]
                {
                    { 1, "https://m.media-amazon.com/images/I/61kjuGfZyML.jpg", new DateTime(1949, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "1984" },
                    { 2, "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg", new DateTime(1953, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Fahrenheit 451" },
                    { 3, "https://images.cdn1.buscalibre.com/fit-in/360x360/68/e0/68e0aac2ed0bfe4c39e0cf16663a5918.jpg", new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rebelión en la granja" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_autorId",
                table: "Libros",
                column: "autorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
