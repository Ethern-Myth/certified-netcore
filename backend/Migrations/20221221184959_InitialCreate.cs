using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Name);
                });

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
                    Desc = table.Column<string>(type: "text", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    InStock = table.Column<bool>(type: "boolean", nullable: false),
                    PDTypeID = table.Column<int>(type: "integer", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
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
                name: "shipping",
                columns: table => new
                {
                    ShippingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerID = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressLine1 = table.Column<string>(type: "text", nullable: false),
                    AddressLine2 = table.Column<string>(type: "text", nullable: true),
                    Suburb = table.Column<string>(type: "text", nullable: false),
                    Town = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping", x => x.ShippingID);
                    table.ForeignKey(
                        name: "FK_shipping_Countries_Name",
                        column: x => x.Name,
                        principalTable: "Countries",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipping_customer_CustomerID",
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

            migrationBuilder.CreateTable(
                name: "delivery",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_delivery_order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Name", "Code" },
                values: new object[,]
                {
                    { "Afghanistan", "AF" },
                    { "Åland Islands", "AX" },
                    { "Albania", "AL" },
                    { "Algeria", "DZ" },
                    { "American Samoa", "AS" },
                    { "AndorrA", "AD" },
                    { "Angola", "AO" },
                    { "Anguilla", "AI" },
                    { "Antarctica", "AQ" },
                    { "Antigua and Barbuda", "AG" },
                    { "Argentina", "AR" },
                    { "Armenia", "AM" },
                    { "Aruba", "AW" },
                    { "Australia", "AU" },
                    { "Austria", "AT" },
                    { "Azerbaijan", "AZ" },
                    { "Bahamas", "BS" },
                    { "Bahrain", "BH" },
                    { "Bangladesh", "BD" },
                    { "Barbados", "BB" },
                    { "Belarus", "BY" },
                    { "Belgium", "BE" },
                    { "Belize", "BZ" },
                    { "Benin", "BJ" },
                    { "Bermuda", "BM" },
                    { "Bhutan", "BT" },
                    { "Bolivia", "BO" },
                    { "Bosnia and Herzegovina", "BA" },
                    { "Botswana", "BW" },
                    { "Bouvet Island", "BV" },
                    { "Brazil", "BR" },
                    { "British Indian Ocean Territory", "IO" },
                    { "Brunei Darussalam", "BN" },
                    { "Bulgaria", "BG" },
                    { "Burkina Faso", "BF" },
                    { "Burundi", "BI" },
                    { "Cambodia", "KH" },
                    { "Cameroon", "CM" },
                    { "Canada", "CA" },
                    { "Cape Verde", "CV" },
                    { "Cayman Islands", "KY" },
                    { "Central African Republic", "CF" },
                    { "Chad", "TD" },
                    { "Chile", "CL" },
                    { "China", "CN" },
                    { "Christmas Island", "CX" },
                    { "Cocos (Keeling) Islands", "CC" },
                    { "Colombia", "CO" },
                    { "Comoros", "KM" },
                    { "Congo", "CG" },
                    { "Congo, The Democratic Republic of the", "CD" },
                    { "Cook Islands", "CK" },
                    { "Costa Rica", "CR" },
                    { "Cote D'Ivoire", "CI" },
                    { "Croatia", "HR" },
                    { "Cuba", "CU" },
                    { "Cyprus", "CY" },
                    { "Czech Republic", "CZ" },
                    { "Denmark", "DK" },
                    { "Djibouti", "DJ" },
                    { "Dominica", "DM" },
                    { "Dominican Republic", "DO" },
                    { "Ecuador", "EC" },
                    { "Egypt", "EG" },
                    { "El Salvador", "SV" },
                    { "Equatorial Guinea", "GQ" },
                    { "Eritrea", "ER" },
                    { "Estonia", "EE" },
                    { "Ethiopia", "ET" },
                    { "Falkland Islands (Malvinas)", "FK" },
                    { "Faroe Islands", "FO" },
                    { "Fiji", "FJ" },
                    { "Finland", "FI" },
                    { "France", "FR" },
                    { "French Guiana", "GF" },
                    { "French Polynesia", "PF" },
                    { "French Southern Territories", "TF" },
                    { "Gabon", "GA" },
                    { "Gambia", "GM" },
                    { "Georgia", "GE" },
                    { "Germany", "DE" },
                    { "Ghana", "GH" },
                    { "Gibraltar", "GI" },
                    { "Greece", "GR" },
                    { "Greenland", "GL" },
                    { "Grenada", "GD" },
                    { "Guadeloupe", "GP" },
                    { "Guam", "GU" },
                    { "Guatemala", "GT" },
                    { "Guernsey", "GG" },
                    { "Guinea", "GN" },
                    { "Guinea-Bissau", "GW" },
                    { "Guyana", "GY" },
                    { "Haiti", "HT" },
                    { "Heard Island and Mcdonald Islands", "HM" },
                    { "Holy See (Vatican City State)", "VA" },
                    { "Honduras", "HN" },
                    { "Hong Kong", "HK" },
                    { "Hungary", "HU" },
                    { "Iceland", "IS" },
                    { "India", "IN" },
                    { "Indonesia", "ID" },
                    { "Iran, Islamic Republic Of", "IR" },
                    { "Iraq", "IQ" },
                    { "Ireland", "IE" },
                    { "Isle of Man", "IM" },
                    { "Israel", "IL" },
                    { "Italy", "IT" },
                    { "Jamaica", "JM" },
                    { "Japan", "JP" },
                    { "Jersey", "JE" },
                    { "Jordan", "JO" },
                    { "Kazakhstan", "KZ" },
                    { "Kenya", "KE" },
                    { "Kiribati", "KI" },
                    { "Korea, Democratic People'S Republic of", "KP" },
                    { "Korea, Republic of", "KR" },
                    { "Kuwait", "KW" },
                    { "Kyrgyzstan", "KG" },
                    { "Lao People'S Democratic Republic", "LA" },
                    { "Latvia", "LV" },
                    { "Lebanon", "LB" },
                    { "Lesotho", "LS" },
                    { "Liberia", "LR" },
                    { "Libyan Arab Jamahiriya", "LY" },
                    { "Liechtenstein", "LI" },
                    { "Lithuania", "LT" },
                    { "Luxembourg", "LU" },
                    { "Macao", "MO" },
                    { "Macedonia, The Former Yugoslav Republic of", "MK" },
                    { "Madagascar", "MG" },
                    { "Malawi", "MW" },
                    { "Malaysia", "MY" },
                    { "Maldives", "MV" },
                    { "Mali", "ML" },
                    { "Malta", "MT" },
                    { "Marshall Islands", "MH" },
                    { "Martinique", "MQ" },
                    { "Mauritania", "MR" },
                    { "Mauritius", "MU" },
                    { "Mayotte", "YT" },
                    { "Mexico", "MX" },
                    { "Micronesia, Federated States of", "FM" },
                    { "Moldova, Republic of", "MD" },
                    { "Monaco", "MC" },
                    { "Mongolia", "MN" },
                    { "Montserrat", "MS" },
                    { "Morocco", "MA" },
                    { "Mozambique", "MZ" },
                    { "Myanmar", "MM" },
                    { "Namibia", "NA" },
                    { "Nauru", "NR" },
                    { "Nepal", "NP" },
                    { "Netherlands", "NL" },
                    { "Netherlands Antilles", "AN" },
                    { "New Caledonia", "NC" },
                    { "New Zealand", "NZ" },
                    { "Nicaragua", "NI" },
                    { "Niger", "NE" },
                    { "Nigeria", "NG" },
                    { "Niue", "NU" },
                    { "Norfolk Island", "NF" },
                    { "Northern Mariana Islands", "MP" },
                    { "Norway", "NO" },
                    { "Oman", "OM" },
                    { "Pakistan", "PK" },
                    { "Palau", "PW" },
                    { "Palestinian Territory, Occupied", "PS" },
                    { "Panama", "PA" },
                    { "Papua New Guinea", "PG" },
                    { "Paraguay", "PY" },
                    { "Peru", "PE" },
                    { "Philippines", "PH" },
                    { "Pitcairn", "PN" },
                    { "Poland", "PL" },
                    { "Portugal", "PT" },
                    { "Puerto Rico", "PR" },
                    { "Qatar", "QA" },
                    { "Reunion", "RE" },
                    { "Romania", "RO" },
                    { "Russian Federation", "RU" },
                    { "RWANDA", "RW" },
                    { "Saint Helena", "SH" },
                    { "Saint Kitts and Nevis", "KN" },
                    { "Saint Lucia", "LC" },
                    { "Saint Pierre and Miquelon", "PM" },
                    { "Saint Vincent and the Grenadines", "VC" },
                    { "Samoa", "WS" },
                    { "San Marino", "SM" },
                    { "Sao Tome and Principe", "ST" },
                    { "Saudi Arabia", "SA" },
                    { "Senegal", "SN" },
                    { "Serbia and Montenegro", "CS" },
                    { "Seychelles", "SC" },
                    { "Sierra Leone", "SL" },
                    { "Singapore", "SG" },
                    { "Slovakia", "SK" },
                    { "Slovenia", "SI" },
                    { "Solomon Islands", "SB" },
                    { "Somalia", "SO" },
                    { "South Africa", "ZA" },
                    { "South Georgia and the South Sandwich Islands", "GS" },
                    { "Spain", "ES" },
                    { "Sri Lanka", "LK" },
                    { "Sudan", "SD" },
                    { "Suriname", "SR" },
                    { "Svalbard and Jan Mayen", "SJ" },
                    { "Swaziland", "SZ" },
                    { "Sweden", "SE" },
                    { "Switzerland", "CH" },
                    { "Syrian Arab Republic", "SY" },
                    { "Taiwan, Province of China", "TW" },
                    { "Tajikistan", "TJ" },
                    { "Tanzania, United Republic of", "TZ" },
                    { "Thailand", "TH" },
                    { "Timor-Leste", "TL" },
                    { "Togo", "TG" },
                    { "Tokelau", "TK" },
                    { "Tonga", "TO" },
                    { "Trinidad and Tobago", "TT" },
                    { "Tunisia", "TN" },
                    { "Turkey", "TR" },
                    { "Turkmenistan", "TM" },
                    { "Turks and Caicos Islands", "TC" },
                    { "Tuvalu", "TV" },
                    { "Uganda", "UG" },
                    { "Ukraine", "UA" },
                    { "United Arab Emirates", "AE" },
                    { "United Kingdom", "GB" },
                    { "United States", "US" },
                    { "United States Minor Outlying Islands", "UM" },
                    { "Uruguay", "UY" },
                    { "Uzbekistan", "UZ" },
                    { "Vanuatu", "VU" },
                    { "Venezuela", "VE" },
                    { "Viet Nam", "VN" },
                    { "Virgin Islands, British", "VG" },
                    { "Virgin Islands, U.S.", "VI" },
                    { "Wallis and Futuna", "WF" },
                    { "Western Sahara", "EH" },
                    { "Yemen", "YE" },
                    { "Zambia", "ZM" },
                    { "Zimbabwe", "ZW" }
                });

            migrationBuilder.InsertData(
                table: "product_type",
                columns: new[] { "PDTypeID", "Category" },
                values: new object[,]
                {
                    { 1, "Beverages" },
                    { 2, "Grains" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "CustomerID", "DateAdded", "DateUpdated", "Email", "Name", "Password", "Phone", "RoleID", "Status", "Surname" },
                values: new object[] { new Guid("b0a8a448-272a-4e92-9509-e43b963e9395"), new DateTimeOffset(new DateTime(2022, 12, 21, 18, 49, 58, 666, DateTimeKind.Unspecified).AddTicks(3105), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 12, 21, 18, 49, 58, 666, DateTimeKind.Unspecified).AddTicks(3109), new TimeSpan(0, 0, 0, 0, 0)), "john@mail.com", "John", "doe100", "555-555-5555", 1, true, "Doe" });

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

            migrationBuilder.CreateIndex(
                name: "IX_shipping_CustomerID",
                table: "shipping",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_Name",
                table: "shipping",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "shipping");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "customer_collection");

            migrationBuilder.DropTable(
                name: "customer_product");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
