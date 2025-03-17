using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mas1.Migrations
{
    /// <inheritdoc />
    public partial class AnotherOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupSize = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.GuestID);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusinessAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CVR = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderID);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ProviderID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceID);
                    table.ForeignKey(
                        name: "FK_Experiences_Providers_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Providers",
                        principalColumn: "ProviderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedExperiences",
                columns: table => new
                {
                    SharedExperienceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExperienceName = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExperienceId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedExperiences", x => x.SharedExperienceID);
                    table.ForeignKey(
                        name: "FK_SharedExperiences_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SharedExperienceID = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Guest_GuestID",
                        column: x => x.GuestID,
                        principalTable: "Guest",
                        principalColumn: "GuestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_SharedExperiences_SharedExperienceID",
                        column: x => x.SharedExperienceID,
                        principalTable: "SharedExperiences",
                        principalColumn: "SharedExperienceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceBookings",
                columns: table => new
                {
                    ExperienceBookingID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExperienceID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservationID = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceBookings", x => x.ExperienceBookingID);
                    table.ForeignKey(
                        name: "FK_ExperienceBookings_Experiences_ExperienceID",
                        column: x => x.ExperienceID,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceBookings_Guest_GuestID",
                        column: x => x.GuestID,
                        principalTable: "Guest",
                        principalColumn: "GuestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceBookings_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "DiscountPercentage", "GroupSize" },
                values: new object[,]
                {
                    { 1, 10, 10 },
                    { 2, 15, 15 }
                });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestID", "Age", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 22, 54, "John Watson", "8888 8888" },
                    { 23, 34, "Sherlock Holmes", "2222 2222" },
                    { 24, 25, "Lara Croft", "1234 5678" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderID", "BusinessAddress", "CVR", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Finlands Gade 27", "98765432", "98765432" },
                    { 2, "Finlands Gade 25", "564323567", "35246543" },
                    { 3, "Finlands Gade 22", "42356754", "5465436" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceID", "Description", "Name", "Price", "ProviderID" },
                values: new object[,]
                {
                    { 1, "Jumping out of a plane", "Skydiving", 250, 1 },
                    { 2, "Swimming with sharks", "Scuba Diving", 200, 2 },
                    { 3, "Hiking above the clouds", "Mountain Hiking", 100, 3 }
                });

            migrationBuilder.InsertData(
                table: "SharedExperiences",
                columns: new[] { "SharedExperienceID", "Date", "DiscountId", "ExperienceId", "ExperienceName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Test 1" },
                    { 2, new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Test 2" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationID", "GuestID", "SharedExperienceID" },
                values: new object[,]
                {
                    { 1, 22, 1 },
                    { 2, 23, 2 },
                    { 3, 24, 1 }
                });

            migrationBuilder.InsertData(
                table: "ExperienceBookings",
                columns: new[] { "ExperienceBookingID", "ExperienceID", "GuestID", "ReservationID" },
                values: new object[,]
                {
                    { 1, 1, 22, 1 },
                    { 2, 2, 23, 2 },
                    { 3, 3, 24, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceBookings_ExperienceID",
                table: "ExperienceBookings",
                column: "ExperienceID");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceBookings_GuestID",
                table: "ExperienceBookings",
                column: "GuestID");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceBookings_ReservationID",
                table: "ExperienceBookings",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ProviderID",
                table: "Experiences",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestID",
                table: "Reservations",
                column: "GuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SharedExperienceID",
                table: "Reservations",
                column: "SharedExperienceID");

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
                name: "Guest");

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
