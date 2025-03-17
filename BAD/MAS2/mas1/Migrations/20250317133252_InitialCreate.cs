using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mas1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedExperiences_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SharedExperienceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_SharedExperiences_SharedExperienceId",
                        column: x => x.SharedExperienceId,
                        principalTable: "SharedExperiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceBookings_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceBookings_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "Code" },
                values: new object[,]
                {
                    { 1, 10.0, "SUMMER2025" },
                    { 2, 15.0, "WINTER2025" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Name", "ProviderId" },
                values: new object[,]
                {
                    { 1, "Skydiving", 0 },
                    { 2, "Scuba Diving", 0 },
                    { 3, "Mountain Hiking", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "GuestName", "SharedExperienceId" },
                values: new object[,]
                {
                    { 1, "John Doe", 0 },
                    { 2, "Jane Smith", 0 },
                    { 3, "Alice Johnson", 0 }
                });

            migrationBuilder.InsertData(
                table: "ExperienceBookings",
                columns: new[] { "Id", "ExperienceId", "ReservationId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "SharedExperiences",
                columns: new[] { "Id", "DiscountId", "ExperienceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceBookings_ExperienceId",
                table: "ExperienceBookings",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceBookings_ReservationId",
                table: "ExperienceBookings",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ProviderId",
                table: "Experiences",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SharedExperienceId",
                table: "Reservations",
                column: "SharedExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedExperiences_DiscountId",
                table: "SharedExperiences",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedExperiences_ExperienceId",
                table: "SharedExperiences",
                column: "ExperienceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceBookings");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SharedExperiences");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
