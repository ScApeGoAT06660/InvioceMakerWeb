using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceMaker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BuyerSellerCreatedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Buyers");
        }
    }
}
