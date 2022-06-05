using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDataForDev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "birthday",
                value: new DateTime(2022, 6, 5, 15, 44, 19, 333, DateTimeKind.Utc).AddTicks(7849));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "birthday", "campus_id", "code", "created_at", "created_by", "email", "gender", "is_deleted", "name", "phone", "status", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 6, 5, 15, 44, 19, 333, DateTimeKind.Utc).AddTicks(7868), 2, "SE140977", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "khoandse140977@fpt.edu.vn", false, false, "Nguyen Dang Khoa", "0123123123", true, null, "" },
                    { 3, new DateTime(2022, 6, 5, 15, 44, 19, 333, DateTimeKind.Utc).AddTicks(7879), 2, "SE14091", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "duyttse140971@fpt.edu.vn", false, false, "Than Thanh Duy", "0123123123", true, null, "" }
                });

            migrationBuilder.InsertData(
                table: "role_users",
                columns: new[] { "id", "capstone_team_id", "created_at", "created_by", "examination_council_id", "is_deleted", "role_id", "updated_at", "updated_by", "user_id" },
                values: new object[,]
                {
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 1, null, "", 2 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 1, null, "", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role_users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "birthday",
                value: new DateTime(2022, 6, 5, 15, 35, 29, 902, DateTimeKind.Utc).AddTicks(9474));
        }
    }
}
