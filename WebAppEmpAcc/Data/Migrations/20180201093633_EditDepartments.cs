using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppEmpAcc.Data.Migrations
{
    public partial class EditDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrnchName",
                table: "Sectors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DprtmntName",
                table: "Sectors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DprtmntName",
                table: "Branchs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrnchName",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "DprtmntName",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "DprtmntName",
                table: "Branchs");
        }
    }
}
