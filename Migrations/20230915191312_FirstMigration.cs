using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombrePersona = table.Column<string>(type: "text", nullable: false),
                    apPatPersona = table.Column<string>(type: "text", nullable: false),
                    apMatPersona = table.Column<string>(type: "text", nullable: false),
                    dniPersona = table.Column<string>(type: "text", nullable: false),
                    sexoPersona = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.id_persona);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    correo_usuario = table.Column<string>(type: "text", nullable: false),
                    nombre_usuario = table.Column<string>(type: "text", nullable: false),
                    contraseña_usuario = table.Column<string>(type: "text", nullable: false),
                    rol_usuario = table.Column<char>(type: "character(1)", nullable: false),
                    personaid_persona = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuario_persona_personaid_persona",
                        column: x => x.personaid_persona,
                        principalTable: "persona",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promocion",
                columns: table => new
                {
                    id_promocion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_promocion = table.Column<string>(type: "text", nullable: false),
                    efecto_promocion = table.Column<string>(type: "text", nullable: false),
                    usuarioid_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocion", x => x.id_promocion);
                    table.ForeignKey(
                        name: "FK_promocion_usuario_usuarioid_usuario",
                        column: x => x.usuarioid_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recibo",
                columns: table => new
                {
                    id_recibo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plan_recibo = table.Column<string>(type: "text", nullable: false),
                    mes_recibo = table.Column<string>(type: "text", nullable: false),
                    fecha_vencimiento = table.Column<string>(type: "text", nullable: false),
                    monto_recibo = table.Column<decimal>(type: "numeric", nullable: false),
                    estado_recibo = table.Column<string>(type: "text", nullable: false),
                    usuarioid_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recibo", x => x.id_recibo);
                    table.ForeignKey(
                        name: "FK_recibo_usuario_usuarioid_usuario",
                        column: x => x.usuarioid_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id_ticket = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipoProblematica_ticket = table.Column<string>(type: "text", nullable: false),
                    descripcion_ticket = table.Column<string>(type: "text", nullable: false),
                    status_ticket = table.Column<string>(type: "text", nullable: false),
                    fecha_ticket = table.Column<string>(type: "text", nullable: false),
                    usuarioid_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.id_ticket);
                    table.ForeignKey(
                        name: "FK_ticket_usuario_usuarioid_usuario",
                        column: x => x.usuarioid_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_promocion_usuarioid_usuario",
                table: "promocion",
                column: "usuarioid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_usuarioid_usuario",
                table: "recibo",
                column: "usuarioid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_usuarioid_usuario",
                table: "ticket",
                column: "usuarioid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_personaid_persona",
                table: "usuario",
                column: "personaid_persona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promocion");

            migrationBuilder.DropTable(
                name: "recibo");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
