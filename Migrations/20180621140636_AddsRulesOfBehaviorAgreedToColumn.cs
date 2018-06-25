using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace lmsextreg.Migrations
{
    public partial class AddsRulesOfBehaviorAgreedToColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RulesOfBehaviorAgreedTo",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RulesOfBehaviorAgreedTo",
                table: "AspNetUsers");
        }
    }
}
