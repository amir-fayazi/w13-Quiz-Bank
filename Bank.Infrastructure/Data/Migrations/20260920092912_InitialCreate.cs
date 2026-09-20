using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bank.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    FailedPasswordAttempts = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardNumber);
                    table.CheckConstraint("CK_Card_Balance_NonNegative", "[Balance] >= 0");
                    table.CheckConstraint("CK_Card_CardNumber_Valid", "LEN([CardNumber]) = 16 AND [CardNumber] NOT LIKE '%[^0-9]%'");
                    table.CheckConstraint("CK_Card_FailedPasswordAttempts", "[FailedPasswordAttempts] >= 0 AND [FailedPasswordAttempts] <= 3");
                    table.CheckConstraint("CK_Card_Password_Valid", "LEN([Password]) = 4 AND [Password] NOT LIKE '%[^0-9]%'");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceCardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    DestinationCardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.CheckConstraint("CK_Transaction_Amount_Positive", "[Amount] > 0");
                    table.CheckConstraint("CK_Transaction_DifferentCards", "[SourceCardNumber] <> [DestinationCardNumber]");
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_DestinationCardNumber",
                        column: x => x.DestinationCardNumber,
                        principalTable: "Cards",
                        principalColumn: "CardNumber");
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_SourceCardNumber",
                        column: x => x.SourceCardNumber,
                        principalTable: "Cards",
                        principalColumn: "CardNumber");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "HolderName", "IsActive", "Password" },
                values: new object[,]
                {
                    { "1000000000000001", 25000m, "Amir Fayazi", true, "1111" },
                    { "1000000000000002", 40000m, "Sara Ahmadi", true, "2222" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "FailedPasswordAttempts", "HolderName", "IsActive", "Password" },
                values: new object[] { "1000000000000003", 18000m, 1, "Reza Mohammadi", true, "3333" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "HolderName", "IsActive", "Password" },
                values: new object[] { "1000000000000004", 65000m, "Neda Karimi", true, "4444" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "FailedPasswordAttempts", "HolderName", "IsActive", "Password" },
                values: new object[] { "1000000000000005", 12000m, 2, "Ali Hosseini", true, "5555" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "HolderName", "IsActive", "Password" },
                values: new object[,]
                {
                    { "1000000000000006", 90000m, "Mina Moradi", true, "6666" },
                    { "1000000000000007", 35000m, "Arman Rahimi", true, "7777" },
                    { "1000000000000008", 50000m, "Leila Akbari", true, "8888" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "FailedPasswordAttempts", "HolderName", "IsActive", "Password" },
                values: new object[,]
                {
                    { "1000000000000009", 30000m, 3, "Blocked Source", false, "9999" },
                    { "1000000000000010", 45000m, 3, "Blocked Destination", false, "1010" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "Amount", "DestinationCardNumber", "IsSuccessful", "SourceCardNumber", "TransactionDate" },
                values: new object[,]
                {
                    { 1, 175m, "1000000000000003", true, "1000000000000001", new DateTime(2026, 9, 1, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 250m, "1000000000000005", true, "1000000000000002", new DateTime(2026, 9, 1, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 325m, "1000000000000007", true, "1000000000000003", new DateTime(2026, 9, 2, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 400m, "1000000000000001", true, "1000000000000004", new DateTime(2026, 9, 2, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 475m, "1000000000000003", true, "1000000000000005", new DateTime(2026, 9, 2, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 550m, "1000000000000005", true, "1000000000000006", new DateTime(2026, 9, 2, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 625m, "1000000000000008", true, "1000000000000007", new DateTime(2026, 9, 3, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 700m, "1000000000000002", true, "1000000000000008", new DateTime(2026, 9, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 775m, "1000000000000004", true, "1000000000000001", new DateTime(2026, 9, 3, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 850m, "1000000000000006", false, "1000000000000002", new DateTime(2026, 9, 3, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 925m, "1000000000000008", true, "1000000000000003", new DateTime(2026, 9, 4, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 1000m, "1000000000000002", true, "1000000000000004", new DateTime(2026, 9, 4, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 1075m, "1000000000000004", true, "1000000000000005", new DateTime(2026, 9, 4, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 1150m, "1000000000000007", true, "1000000000000006", new DateTime(2026, 9, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 1225m, "1000000000000001", true, "1000000000000007", new DateTime(2026, 9, 5, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 1300m, "1000000000000003", true, "1000000000000008", new DateTime(2026, 9, 5, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 1375m, "1000000000000005", true, "1000000000000001", new DateTime(2026, 9, 5, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 1450m, "1000000000000007", true, "1000000000000002", new DateTime(2026, 9, 5, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 1525m, "1000000000000001", true, "1000000000000003", new DateTime(2026, 9, 6, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 100m, "1000000000000003", false, "1000000000000004", new DateTime(2026, 9, 6, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 175m, "1000000000000006", true, "1000000000000005", new DateTime(2026, 9, 6, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 250m, "1000000000000008", true, "1000000000000006", new DateTime(2026, 9, 6, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 325m, "1000000000000002", true, "1000000000000007", new DateTime(2026, 9, 7, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 400m, "1000000000000004", true, "1000000000000008", new DateTime(2026, 9, 7, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 475m, "1000000000000006", true, "1000000000000001", new DateTime(2026, 9, 7, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 550m, "1000000000000008", true, "1000000000000002", new DateTime(2026, 9, 7, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 625m, "1000000000000002", true, "1000000000000003", new DateTime(2026, 9, 8, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 700m, "1000000000000005", true, "1000000000000004", new DateTime(2026, 9, 8, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 775m, "1000000000000007", true, "1000000000000005", new DateTime(2026, 9, 8, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 850m, "1000000000000001", false, "1000000000000006", new DateTime(2026, 9, 8, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, 925m, "1000000000000003", true, "1000000000000007", new DateTime(2026, 9, 9, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 1000m, "1000000000000005", true, "1000000000000008", new DateTime(2026, 9, 9, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 1075m, "1000000000000007", true, "1000000000000001", new DateTime(2026, 9, 9, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, 1150m, "1000000000000001", true, "1000000000000002", new DateTime(2026, 9, 9, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, 1225m, "1000000000000004", true, "1000000000000003", new DateTime(2026, 9, 10, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, 1300m, "1000000000000006", true, "1000000000000004", new DateTime(2026, 9, 10, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, 1375m, "1000000000000008", true, "1000000000000005", new DateTime(2026, 9, 10, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, 1450m, "1000000000000002", true, "1000000000000006", new DateTime(2026, 9, 10, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, 1525m, "1000000000000004", true, "1000000000000007", new DateTime(2026, 9, 11, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, 100m, "1000000000000006", false, "1000000000000008", new DateTime(2026, 9, 11, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 41, 175m, "1000000000000008", true, "1000000000000001", new DateTime(2026, 9, 11, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 42, 250m, "1000000000000003", true, "1000000000000002", new DateTime(2026, 9, 11, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 43, 325m, "1000000000000005", true, "1000000000000003", new DateTime(2026, 9, 12, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 44, 400m, "1000000000000007", true, "1000000000000004", new DateTime(2026, 9, 12, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 45, 475m, "1000000000000001", true, "1000000000000005", new DateTime(2026, 9, 12, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 46, 550m, "1000000000000003", true, "1000000000000006", new DateTime(2026, 9, 12, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 47, 625m, "1000000000000005", true, "1000000000000007", new DateTime(2026, 9, 13, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 48, 700m, "1000000000000007", true, "1000000000000008", new DateTime(2026, 9, 13, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 49, 775m, "1000000000000002", true, "1000000000000001", new DateTime(2026, 9, 13, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 50, 850m, "1000000000000004", false, "1000000000000002", new DateTime(2026, 9, 13, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 51, 925m, "1000000000000006", true, "1000000000000003", new DateTime(2026, 9, 14, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 52, 1000m, "1000000000000008", true, "1000000000000004", new DateTime(2026, 9, 14, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 53, 1075m, "1000000000000002", true, "1000000000000005", new DateTime(2026, 9, 14, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 54, 1150m, "1000000000000004", true, "1000000000000006", new DateTime(2026, 9, 14, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 55, 1225m, "1000000000000006", true, "1000000000000007", new DateTime(2026, 9, 15, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 56, 1300m, "1000000000000001", true, "1000000000000008", new DateTime(2026, 9, 15, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 57, 1375m, "1000000000000003", true, "1000000000000001", new DateTime(2026, 9, 15, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 58, 1450m, "1000000000000005", true, "1000000000000002", new DateTime(2026, 9, 15, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 59, 1525m, "1000000000000007", true, "1000000000000003", new DateTime(2026, 9, 16, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 60, 100m, "1000000000000001", false, "1000000000000004", new DateTime(2026, 9, 16, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 61, 175m, "1000000000000003", true, "1000000000000005", new DateTime(2026, 9, 16, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 62, 250m, "1000000000000005", true, "1000000000000006", new DateTime(2026, 9, 16, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 63, 325m, "1000000000000008", true, "1000000000000007", new DateTime(2026, 9, 17, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 64, 400m, "1000000000000002", true, "1000000000000008", new DateTime(2026, 9, 17, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 65, 475m, "1000000000000004", true, "1000000000000001", new DateTime(2026, 9, 17, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 66, 550m, "1000000000000006", true, "1000000000000002", new DateTime(2026, 9, 17, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 67, 625m, "1000000000000008", true, "1000000000000003", new DateTime(2026, 9, 18, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 68, 700m, "1000000000000002", true, "1000000000000004", new DateTime(2026, 9, 18, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 69, 775m, "1000000000000004", true, "1000000000000005", new DateTime(2026, 9, 18, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 70, 850m, "1000000000000007", false, "1000000000000006", new DateTime(2026, 9, 18, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 71, 925m, "1000000000000001", true, "1000000000000007", new DateTime(2026, 9, 19, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 72, 1000m, "1000000000000003", true, "1000000000000008", new DateTime(2026, 9, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 73, 1075m, "1000000000000005", true, "1000000000000001", new DateTime(2026, 9, 19, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 74, 1150m, "1000000000000007", true, "1000000000000002", new DateTime(2026, 9, 19, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 75, 1225m, "1000000000000001", true, "1000000000000003", new DateTime(2026, 9, 20, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 76, 1300m, "1000000000000003", true, "1000000000000004", new DateTime(2026, 9, 20, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 77, 1375m, "1000000000000006", true, "1000000000000005", new DateTime(2026, 9, 20, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 78, 1450m, "1000000000000008", true, "1000000000000006", new DateTime(2026, 9, 20, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 79, 1525m, "1000000000000002", true, "1000000000000007", new DateTime(2026, 9, 21, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 80, 100m, "1000000000000004", false, "1000000000000008", new DateTime(2026, 9, 21, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationCardNumber",
                table: "Transactions",
                column: "DestinationCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceCardNumber",
                table: "Transactions",
                column: "SourceCardNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
