using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_type",
                columns: table => new
                {
                    PDTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type", x => x.PDTypeID);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    InStock = table.Column<bool>(type: "boolean", nullable: false),
                    PDTypeID = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_product_product_type_PDTypeID",
                        column: x => x.PDTypeID,
                        principalTable: "product_type",
                        principalColumn: "PDTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_customer_roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_product",
                columns: table => new
                {
                    CPID = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uuid", nullable: false),
                    Products = table.Column<string>(type: "text", nullable: false),
                    Subtotal = table.Column<double>(type: "double precision", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_product", x => x.CPID);
                    table.ForeignKey(
                        name: "FK_customer_product_customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_collection",
                columns: table => new
                {
                    CCID = table.Column<Guid>(type: "uuid", nullable: false),
                    CPID = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_collection", x => x.CCID);
                    table.ForeignKey(
                        name: "FK_customer_collection_customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_collection_customer_product_CPID",
                        column: x => x.CPID,
                        principalTable: "customer_product",
                        principalColumn: "CPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ItemCount = table.Column<int>(type: "integer", nullable: false),
                    OrderTotal = table.Column<double>(type: "double precision", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    CCID = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_order_customer_collection_CCID",
                        column: x => x.CCID,
                        principalTable: "customer_collection",
                        principalColumn: "CCID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "CustomerID", "DateAdded", "DateUpdated", "Email", "Name", "Password", "Phone", "RoleID", "Status", "Surname" },
                values: new object[] { new Guid("6b103cd3-7903-4afd-8077-62ecc7e6a70c"), new DateTimeOffset(new DateTime(2022, 12, 18, 20, 45, 20, 116, DateTimeKind.Unspecified).AddTicks(9677), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 12, 18, 20, 45, 20, 116, DateTimeKind.Unspecified).AddTicks(9681), new TimeSpan(0, 0, 0, 0, 0)), "john@mail.com", "John", "doe100", "555-555-5555", 1, true, "Doe" });

            migrationBuilder.CreateIndex(
                name: "IX_customer_RoleID",
                table: "customer",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_customer_collection_CPID",
                table: "customer_collection",
                column: "CPID");

            migrationBuilder.CreateIndex(
                name: "IX_customer_collection_CustomerID",
                table: "customer_collection",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_customer_product_CustomerID",
                table: "customer_product",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_order_CCID",
                table: "order",
                column: "CCID");

            migrationBuilder.CreateIndex(
                name: "IX_product_PDTypeID",
                table: "product",
                column: "PDTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "customer_collection");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "customer_product");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
