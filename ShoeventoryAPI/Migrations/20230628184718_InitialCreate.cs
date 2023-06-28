using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoeCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeCollectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoeCollections_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    ShoeCollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_ShoeCollections_ShoeCollectionId",
                        column: x => x.ShoeCollectionId,
                        principalTable: "ShoeCollections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeSize = table.Column<int>(type: "int", nullable: false),
                    ShoeColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeQuantity = table.Column<int>(type: "int", nullable: false),
                    ShoePrice = table.Column<double>(type: "float", nullable: false),
                    ShoeCollectionId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoes_ShoeCollections_ShoeCollectionId",
                        column: x => x.ShoeCollectionId,
                        principalTable: "ShoeCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shoes_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoldShoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    ShoeSize = table.Column<double>(type: "float", nullable: false),
                    ShoeQuantity = table.Column<int>(type: "int", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldShoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldShoes_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoldShoes_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeCollections_MerchantId",
                table: "ShoeCollections",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ShoeCollectionId",
                table: "Shoes",
                column: "ShoeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_TransactionId",
                table: "Shoes",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldShoes_ShoeId",
                table: "SoldShoes",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldShoes_TransactionId",
                table: "SoldShoes",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantId",
                table: "Transactions",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ShoeCollectionId",
                table: "Transactions",
                column: "ShoeCollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldShoes");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ShoeCollections");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
