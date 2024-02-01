using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS2._0.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorID);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryInfos",
                columns: table => new
                {
                    SecondaryInfoID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlternateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateEmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryInfos", x => x.SecondaryInfoID);
                    table.ForeignKey(
                        name: "FK_SecondaryInfos_Visitors_VisitorID",
                        column: x => x.VisitorID,
                        principalTable: "Visitors",
                        principalColumn: "VisitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentVisitID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedDepart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitID);
                    table.ForeignKey(
                        name: "FK_Visits_Visitors_VisitorID",
                        column: x => x.VisitorID,
                        principalTable: "Visitors",
                        principalColumn: "VisitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationGenerated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NotificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Visits_VisitID",
                        column: x => x.VisitID,
                        principalTable: "Visits",
                        principalColumn: "VisitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "URLManagements",
                columns: table => new
                {
                    URLID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenerateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    URLStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URLType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLManagements", x => x.URLID);
                    table.ForeignKey(
                        name: "FK_URLManagements_Visits_VisitID",
                        column: x => x.VisitID,
                        principalTable: "Visits",
                        principalColumn: "VisitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VisitID",
                table: "Notifications",
                column: "VisitID");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryInfos_VisitorID",
                table: "SecondaryInfos",
                column: "VisitorID");

            migrationBuilder.CreateIndex(
                name: "IX_URLManagements_VisitID",
                table: "URLManagements",
                column: "VisitID");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitorID",
                table: "Visits",
                column: "VisitorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SecondaryInfos");

            migrationBuilder.DropTable(
                name: "URLManagements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Visitors");
        }
    }
}
