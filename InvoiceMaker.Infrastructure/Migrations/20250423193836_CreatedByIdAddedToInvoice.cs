using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceMaker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAddedToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CreatedById",
                table: "Invoices",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CreatedById",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Invoices");
        }
    }
}
