using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkOrders_DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaborRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TechsCount = table.Column<int>(type: "int", nullable: false),
                    LbrRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leadtechs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leadtechs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weekdays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weekdays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeadtechId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rush = table.Column<bool>(type: "bit", nullable: false),
                    ReqDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Techs = table.Column<int>(type: "int", nullable: false),
                    WtyLbr = table.Column<bool>(type: "bit", nullable: false),
                    WtyParts = table.Column<bool>(type: "bit", nullable: false),
                    LbrHrs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0a447cfb-c6a1-4c27-9519-ab4c808763b4"), "North" },
                    { new Guid("42972f19-99fe-4b0c-9725-bafa51dbfa82"), "Northeast" },
                    { new Guid("5ec1e23d-40dd-4f84-9e5f-ffe8fecdc4c1"), "West" },
                    { new Guid("6ec89009-d74d-4b20-badc-dccbdf8f6231"), "Southeast" },
                    { new Guid("6fce05e4-e206-42ec-9f17-a74e1be771c6"), "Northwest" },
                    { new Guid("81047810-bb96-4941-ae93-c6b2b4252a4f"), "Southwest" },
                    { new Guid("a0f3d9a2-ec2c-49b0-b7bb-71ee41df1c8c"), "Central" },
                    { new Guid("e0c2ca9f-70b7-4845-ac1e-3ba43724f455"), "South" },
                    { new Guid("f126e1bb-c067-4a9b-8b34-68ab0e780469"), "East" }
                });

            migrationBuilder.InsertData(
                table: "LaborRates",
                columns: new[] { "Id", "LbrRate", "TechsCount" },
                values: new object[,]
                {
                    { new Guid("b17a2779-169c-4d48-b9e7-8e118345b386"), 195m, 3 },
                    { new Guid("d61898b6-e5bd-4295-b370-c8b2c0e17459"), 140m, 2 },
                    { new Guid("dae17957-8354-4e0d-8619-665a7a73e871"), 80m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Leadtechs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15b6718a-6c69-499e-a6a5-118fb8fddabf"), "Ling" },
                    { new Guid("24d9d5a3-531c-456e-9922-c8f6e2b8a206"), "Cartier" },
                    { new Guid("8e744b13-68d3-497c-876c-161a8b142e23"), "Burton" },
                    { new Guid("c182f8f4-07ec-4e3c-9505-5ff7ea388cfc"), "Khan" },
                    { new Guid("c615542d-b11c-44a6-9c2d-b70118812cf9"), "Lopez" },
                    { new Guid("c61da18d-8dab-4e80-bb3e-e588bdfbe768"), "Michner" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2a7a7db9-3fa8-4fc3-92cf-0d1804d86189"), "Warranty" },
                    { new Guid("512f0a14-7a92-422a-8642-81901269e8f7"), "Credit" },
                    { new Guid("8871bee7-5e34-4a3d-9498-72c1590ed2c5"), "P.O." },
                    { new Guid("cfca6b93-c9ba-4a9b-b5fb-eb2158e9d60c"), "C.O.D." },
                    { new Guid("d68db0c8-1a1c-4a3a-ba37-c2e1c37895a0"), "Account" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13e018f2-e406-4f1c-81ef-4b3b960fc722"), "Replace" },
                    { new Guid("2d89d47a-e12b-4ca7-875f-5028159326da"), "Repair" },
                    { new Guid("3a3f036b-7753-45cb-9853-d0cc73659f4f"), "Assess" },
                    { new Guid("bad19515-0e60-491a-a197-0154e0602d38"), "Install" },
                    { new Guid("f547cb61-be5e-44a5-a86c-9e037cb5d724"), "Deliver" }
                });

            migrationBuilder.InsertData(
                table: "Weekdays",
                columns: new[] { "Id", "DayOfWeek", "Name" },
                values: new object[,]
                {
                    { new Guid("3e5a02de-2f3f-4ef0-9355-acb6c8b5a8b1"), 6, "Sat" },
                    { new Guid("5b5f2880-fd6b-4c33-bbb1-83c7bd56e1cb"), 3, "Wed" },
                    { new Guid("5f325a05-57be-4689-a30e-22705b763571"), 5, "Fri" },
                    { new Guid("cf4060f2-4ad0-4560-9a2f-77886802c637"), 4, "Thu" },
                    { new Guid("e3b1fbd3-c6f9-4b35-bc6a-53f05b35414c"), 2, "Tue" },
                    { new Guid("edbd799f-cbc2-4dd7-af73-9a1214d7d53f"), 1, "Mon" },
                    { new Guid("f4aae4fd-e33e-436a-9a86-d64612ad9272"), 7, "Sun" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LaborRates_TechsCount",
                table: "LaborRates",
                column: "TechsCount",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leadtechs_Name",
                table: "Leadtechs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Name",
                table: "Payments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Name",
                table: "Services",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weekdays_Name",
                table: "Weekdays",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WO",
                table: "WorkOrders",
                column: "WO",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "LaborRates");

            migrationBuilder.DropTable(
                name: "Leadtechs");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Weekdays");

            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}
