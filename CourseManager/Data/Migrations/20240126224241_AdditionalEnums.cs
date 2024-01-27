using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AdditionalEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Waitlist",
                table: "Sections",
                newName: "WaitListSize");

            migrationBuilder.Sql(@"ALTER TABLE ""Sections"" ALTER COLUMN ""Status"" TYPE integer USING ""Status""::integer;");
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.Sql(@"ALTER TABLE ""Sections"" ALTER COLUMN ""Delivery"" TYPE integer USING ""Delivery""::integer;");
            migrationBuilder.AlterColumn<int>(
                name: "Delivery",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
            
            migrationBuilder.Sql(@"ALTER TABLE ""Sections"" ALTER COLUMN ""Campus"" TYPE integer USING ""Campus""::integer;");
            migrationBuilder.AlterColumn<int>(
                name: "Campus",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WaitListSize",
                table: "Sections",
                newName: "Waitlist");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Delivery",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Campus",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
