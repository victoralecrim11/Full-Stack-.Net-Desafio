using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Infraestructure.Migrations
{
    public partial class MakeIdIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remove a Primary Key (necessário para dropar/remover a coluna)
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            // 2. Remove a coluna 'Id' antiga
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Leads");

            // 3. Adiciona a nova coluna 'Id' como INT e IDENTITY (Auto-Incremento)
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0)
                // ESSA LINHA É CRUCIAL PARA IDENTITY
                .Annotation("SqlServer:Identity", "1, 1");

            // 4. Recria a Chave Primária na nova coluna 'Id'
            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Remove a Primary Key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            // 2. Remove a coluna 'Id' (Identity)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Leads");

            // 3. Adiciona a coluna 'Id' de volta como uniqueidentifier (original)
            // Se o tipo original era uniqueidentifier (como na imagem), usamos uniqueidentifier.
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Leads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()"); // Adiciona o valor padrão para não nulo

            // 4. Recria a Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");
        }
    }
}