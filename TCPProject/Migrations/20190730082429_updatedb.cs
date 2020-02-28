using Microsoft.EntityFrameworkCore.Migrations;

namespace TCPProject.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_applications_ApplicationsId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_datas_categories_CategoryId",
                table: "datas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_datas",
                table: "datas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_applications",
                table: "applications");

            migrationBuilder.RenameTable(
                name: "datas",
                newName: "Datas");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "applications",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_datas_CategoryId",
                table: "Datas",
                newName: "IX_Datas_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_ApplicationsId",
                table: "Categories",
                newName: "IX_Categories_ApplicationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datas",
                table: "Datas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Applications_ApplicationsId",
                table: "Categories",
                column: "ApplicationsId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Datas_Categories_CategoryId",
                table: "Datas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Applications_ApplicationsId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Datas_Categories_CategoryId",
                table: "Datas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Datas",
                table: "Datas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "Datas",
                newName: "datas");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "applications");

            migrationBuilder.RenameIndex(
                name: "IX_Datas_CategoryId",
                table: "datas",
                newName: "IX_datas_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ApplicationsId",
                table: "categories",
                newName: "IX_categories_ApplicationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_datas",
                table: "datas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_applications",
                table: "applications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_applications_ApplicationsId",
                table: "categories",
                column: "ApplicationsId",
                principalTable: "applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_datas_categories_CategoryId",
                table: "datas",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
