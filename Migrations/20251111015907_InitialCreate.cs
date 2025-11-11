using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventoSismicoApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlcancesSismos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcancesSismos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClasificacionesSismos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KmProfundidadDesde = table.Column<double>(type: "REAL", nullable: false),
                    KmProfundidadHasta = table.Column<double>(type: "REAL", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasificacionesSismos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstacionesSismologicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodigoEstacion = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Latitud = table.Column<double>(type: "REAL", nullable: false),
                    Longitud = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacionesSismologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ambito = table.Column<string>(type: "TEXT", nullable: false),
                    NombreEstado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrigenesDeGeneracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrigenesDeGeneracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeDato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Denominacion = table.Column<string>(type: "TEXT", nullable: false),
                    ValorUmbral = table.Column<double>(type: "REAL", nullable: false),
                    NombreUnidadMedida = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeDato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Contraseña = table.Column<string>(type: "TEXT", nullable: false),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sismografos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentificadorSismografo = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstacionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sismografos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sismografos_EstacionesSismologicas_EstacionId",
                        column: x => x.EstacionId,
                        principalTable: "EstacionesSismologicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventosSismicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaHoraFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaHoraOcurrencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LatitudEpicentro = table.Column<double>(type: "REAL", nullable: false),
                    LongitudEpicentro = table.Column<double>(type: "REAL", nullable: false),
                    LatitudHipocentro = table.Column<double>(type: "REAL", nullable: false),
                    LongitudHipocentro = table.Column<double>(type: "REAL", nullable: false),
                    ValorMagnitud = table.Column<double>(type: "REAL", nullable: false),
                    EstadoActualId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlcanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrigenId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasificacionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EsAutoDetectado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosSismicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventosSismicos_AlcancesSismos_AlcanceId",
                        column: x => x.AlcanceId,
                        principalTable: "AlcancesSismos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosSismicos_ClasificacionesSismos_ClasificacionId",
                        column: x => x.ClasificacionId,
                        principalTable: "ClasificacionesSismos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosSismicos_Estados_EstadoActualId",
                        column: x => x.EstadoActualId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosSismicos_OrigenesDeGeneracion_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "OrigenesDeGeneracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaHoraFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesiones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CambiosDeEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaHoraFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FechaHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoResponsableId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoSismicoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CambiosDeEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CambiosDeEstado_Empleados_EmpleadoResponsableId",
                        column: x => x.EmpleadoResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CambiosDeEstado_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CambiosDeEstado_EventosSismicos_EventoSismicoId",
                        column: x => x.EventoSismicoId,
                        principalTable: "EventosSismicos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SeriesTemporales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CondicionAlarma = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaHoraInicioRegistroMuestras = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaHoraRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FrecuenciaMuestreo = table.Column<double>(type: "REAL", nullable: false),
                    SismografoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoSismicoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesTemporales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesTemporales_EventosSismicos_EventoSismicoId",
                        column: x => x.EventoSismicoId,
                        principalTable: "EventosSismicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeriesTemporales_Sismografos_SismografoId",
                        column: x => x.SismografoId,
                        principalTable: "Sismografos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuestrasSismicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaHoraMuestra = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SerieTemporalId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuestrasSismicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuestrasSismicas_SeriesTemporales_SerieTemporalId",
                        column: x => x.SerieTemporalId,
                        principalTable: "SeriesTemporales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetallesMuestrasSismicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    TipoDatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MuestraSismicaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesMuestrasSismicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesMuestrasSismicas_MuestrasSismicas_MuestraSismicaId",
                        column: x => x.MuestraSismicaId,
                        principalTable: "MuestrasSismicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallesMuestrasSismicas_TiposDeDato_TipoDatoId",
                        column: x => x.TipoDatoId,
                        principalTable: "TiposDeDato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CambiosDeEstado_EmpleadoResponsableId",
                table: "CambiosDeEstado",
                column: "EmpleadoResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosDeEstado_EstadoId",
                table: "CambiosDeEstado",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosDeEstado_EventoSismicoId",
                table: "CambiosDeEstado",
                column: "EventoSismicoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMuestrasSismicas_MuestraSismicaId",
                table: "DetallesMuestrasSismicas",
                column: "MuestraSismicaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMuestrasSismicas_TipoDatoId",
                table: "DetallesMuestrasSismicas",
                column: "TipoDatoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosSismicos_AlcanceId",
                table: "EventosSismicos",
                column: "AlcanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosSismicos_ClasificacionId",
                table: "EventosSismicos",
                column: "ClasificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosSismicos_EstadoActualId",
                table: "EventosSismicos",
                column: "EstadoActualId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosSismicos_OrigenId",
                table: "EventosSismicos",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_MuestrasSismicas_SerieTemporalId",
                table: "MuestrasSismicas",
                column: "SerieTemporalId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesTemporales_EventoSismicoId",
                table: "SeriesTemporales",
                column: "EventoSismicoId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesTemporales_SismografoId",
                table: "SeriesTemporales",
                column: "SismografoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_UsuarioId",
                table: "Sesiones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sismografos_EstacionId",
                table: "Sismografos",
                column: "EstacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CambiosDeEstado");

            migrationBuilder.DropTable(
                name: "DetallesMuestrasSismicas");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "MuestrasSismicas");

            migrationBuilder.DropTable(
                name: "TiposDeDato");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "SeriesTemporales");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "EventosSismicos");

            migrationBuilder.DropTable(
                name: "Sismografos");

            migrationBuilder.DropTable(
                name: "AlcancesSismos");

            migrationBuilder.DropTable(
                name: "ClasificacionesSismos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "OrigenesDeGeneracion");

            migrationBuilder.DropTable(
                name: "EstacionesSismologicas");
        }
    }
}
