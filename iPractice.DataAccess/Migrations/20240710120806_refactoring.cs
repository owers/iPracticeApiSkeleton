using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPractice.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Clients_ClientId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Psychologists_PsychologistId",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Psychologists",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PsychologistId",
                table: "Booking",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Booking",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Clients_ClientId",
                table: "Booking",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Psychologists_PsychologistId",
                table: "Booking",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Clients_ClientId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Psychologists_PsychologistId",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Psychologists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<long>(
                name: "PsychologistId",
                table: "Booking",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Booking",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Clients_ClientId",
                table: "Booking",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Psychologists_PsychologistId",
                table: "Booking",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id");
        }
    }
}
