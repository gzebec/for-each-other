using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BPUIO_OneForEachOther.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cv_authentication_schemes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_authentication_schemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cv_countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    IconUrl = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cv_roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cv_cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_cities_cv_countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "cv_countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    AuthenticationSchemeId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lng = table.Column<string>(nullable: true),
                    GdprConsent = table.Column<string>(nullable: true),
                    GdprConsentDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_users_cv_authentication_schemes_AuthenticationSchemeId",
                        column: x => x.AuthenticationSchemeId,
                        principalTable: "cv_authentication_schemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_cv_users_cv_countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "cv_countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_boroughs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_boroughs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_boroughs_cv_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cv_cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_user_notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_user_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_user_notifications_cv_users_UserId",
                        column: x => x.UserId,
                        principalTable: "cv_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_user_roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_user_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_user_roles_cv_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "cv_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_cv_user_roles_cv_users_UserId",
                        column: x => x.UserId,
                        principalTable: "cv_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction,
                        onUpdate: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    BoroughId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lng = table.Column<string>(nullable: true),
                    GdprConsent = table.Column<string>(nullable: true),
                    GdprConsentDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_orders_cv_boroughs_BoroughId",
                        column: x => x.BoroughId,
                        principalTable: "cv_boroughs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_user_boroughs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    BoroughId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_user_boroughs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_user_boroughs_cv_boroughs_BoroughId",
                        column: x => x.BoroughId,
                        principalTable: "cv_boroughs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_cv_user_boroughs_cv_users_UserId",
                        column: x => x.UserId,
                        principalTable: "cv_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cv_order_details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    Item = table.Column<string>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cv_order_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cv_order_details_cv_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "cv_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cv_boroughs_CityId",
                table: "cv_boroughs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_cities_CountryId",
                table: "cv_cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_order_details_OrderId",
                table: "cv_order_details",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_orders_BoroughId",
                table: "cv_orders",
                column: "BoroughId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_user_boroughs_BoroughId",
                table: "cv_user_boroughs",
                column: "BoroughId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_user_boroughs_UserId",
                table: "cv_user_boroughs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_user_notifications_UserId",
                table: "cv_user_notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_user_roles_RoleId",
                table: "cv_user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_user_roles_UserId",
                table: "cv_user_roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_users_AuthenticationSchemeId",
                table: "cv_users",
                column: "AuthenticationSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_cv_users_CountryId",
                table: "cv_users",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cv_order_details");

            migrationBuilder.DropTable(
                name: "cv_user_boroughs");

            migrationBuilder.DropTable(
                name: "cv_user_notifications");

            migrationBuilder.DropTable(
                name: "cv_user_roles");

            migrationBuilder.DropTable(
                name: "cv_orders");

            migrationBuilder.DropTable(
                name: "cv_roles");

            migrationBuilder.DropTable(
                name: "cv_users");

            migrationBuilder.DropTable(
                name: "cv_boroughs");

            migrationBuilder.DropTable(
                name: "cv_authentication_schemes");

            migrationBuilder.DropTable(
                name: "cv_cities");

            migrationBuilder.DropTable(
                name: "cv_countries");
        }
    }
}
