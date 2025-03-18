using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mas1.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PermitPDF",
                table: "Providers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Experiences",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 1,
                column: "Price",
                value: 250.0);

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 2,
                column: "Price",
                value: 200.0);

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 3,
                column: "Price",
                value: 100.0);

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ProviderID",
                keyValue: 1,
                column: "PermitPDF",
                value: null);

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ProviderID",
                keyValue: 2,
                column: "PermitPDF",
                value: null);

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ProviderID",
                keyValue: 3,
                column: "PermitPDF",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermitPDF",
                table: "Providers");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Experiences",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 1,
                column: "Price",
                value: 250);

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 2,
                column: "Price",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExperienceID",
                keyValue: 3,
                column: "Price",
                value: 100);
        }
    }
}
