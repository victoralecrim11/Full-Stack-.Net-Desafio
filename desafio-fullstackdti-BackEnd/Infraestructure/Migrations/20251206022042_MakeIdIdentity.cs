using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateIdToIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remove a Primary Key existente (necessário para dropar/remover a coluna)
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            // 2. Remove a coluna 'Id' (Opcional, se precisar alterar o tipo/constraints também)
            // No caso de alterar para Identity, esta etapa é obrigatória no SQL Server.
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Leads");

            // 3. Adiciona a nova coluna 'Id' com a propriedade Identity (Auto-Incremento)
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0) // Valor padrão temporário (não será usado)
                .Annotation("SqlServer:Identity", "1, 1"); // <-- Configura como IDENTITY

            // 4. Recria a Chave Primária na nova coluna 'Id'
            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // O processo de Down é mais complexo em caso de dados, mas para reverter a migração:

            // 1. Remove a Primary Key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            // 2. Remove a coluna 'Id' (Identity)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Leads");

            // 3. Adiciona a coluna 'Id' de volta SEM a propriedade Identity
            // ATENÇÃO: Se o tipo original era uniqueidentifier (como sugerido em um erro anterior)
            // você precisará alterar o tipo abaixo de 'int' para o tipo original.
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 4. Recria a Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");
        }
    }
}