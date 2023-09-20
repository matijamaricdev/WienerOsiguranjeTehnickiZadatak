using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WienerOsiguranjeTehnickiZadatak.Data.Migrations
{
    public partial class AddingDropdownValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Domicilnost", "Tip", "Domaći");
            migrationBuilder.InsertData("Domicilnost", "Tip", "Strani");
            migrationBuilder.InsertData("TipPartnera", "Tip", "Pravna osoba");
            migrationBuilder.InsertData("TipPartnera", "Tip", "Fizička osoba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
