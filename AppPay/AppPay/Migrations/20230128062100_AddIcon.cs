using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppPay.Migrations
{
    public partial class AddIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "FeaturesModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "FeaturesModels");
        }
    }
}
