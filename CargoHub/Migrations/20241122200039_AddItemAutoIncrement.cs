using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHub.Migrations
{
    /// <inheritdoc />
    public partial class AddItemAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
            name: "ItemNew",
            columns: table => new
            {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Uid = table.Column<string>(type: "TEXT", nullable: false),
            Code = table.Column<string>(type: "TEXT", nullable: false),
            Description = table.Column<string>(type: "TEXT", nullable: false),
            Short_Description = table.Column<string>(type: "TEXT", nullable: false),
            Upc_Code = table.Column<string>(type: "TEXT", nullable: false),
            Model_Number = table.Column<string>(type: "TEXT", nullable: false),
            Commodity_Code = table.Column<string>(type: "TEXT", nullable: false),
            Item_Line = table.Column<int>(type: "INTEGER", nullable: false),
            Item_Group = table.Column<int>(type: "INTEGER", nullable: false),
            Item_Type = table.Column<int>(type: "INTEGER", nullable: false),
            Unit_Purchase_Quantity = table.Column<int>(type: "INTEGER", nullable: false),
            Unit_Order_Quantity = table.Column<int>(type: "INTEGER", nullable: false),
            Pack_Order_Quantity = table.Column<int>(type: "INTEGER", nullable: false),
            Supplier_Id = table.Column<int>(type: "INTEGER", nullable: false),
            Supplier_Code = table.Column<string>(type: "TEXT", nullable: false),
            Supplier_Part_Number = table.Column<string>(type: "TEXT", nullable: false),
            Created_At = table.Column<DateTime>(type: "TEXT", nullable: false),
            Updated_At = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Item", x => x.Id);
            });

            // Copy data from the old table to the new table
            migrationBuilder.Sql("INSERT INTO ItemNew (Uid, Code, Description, Short_Description, Upc_Code, Model_Number, Commodity_Code, Item_Line, Item_Group, Item_Type, Unit_Purchase_Quantity, Unit_Order_Quantity, Pack_Order_Quantity, Supplier_Id, Supplier_Code, Supplier_Part_Number, Created_At, Updated_At) SELECT Uid, Code, Description, Short_Description, Upc_Code, Model_Number, Commodity_Code, Item_Line, Item_Group, Item_Type, Unit_Purchase_Quantity, Unit_Order_Quantity, Pack_Order_Quantity, Supplier_Id, Supplier_Code, Supplier_Part_Number, Created_At, Updated_At FROM Item");

            // Drop the old table
            migrationBuilder.DropTable(name: "Item");

            // Rename the new table to the original table name
            migrationBuilder.RenameTable(name: "ItemNew", newName: "Item");
        }
    }
}
