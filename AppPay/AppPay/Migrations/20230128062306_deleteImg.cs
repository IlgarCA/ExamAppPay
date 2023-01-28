using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppPay.Migrations
{
    public partial class deleteImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "FeaturesModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "FeaturesModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
