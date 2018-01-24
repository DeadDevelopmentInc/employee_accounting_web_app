using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppEmpAcc.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentRoute",
                table: "AspNetUsers",
                newName: "Sector");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "AspNetUsers",
                newName: "DepartmentRoute");
        }
    }
}
