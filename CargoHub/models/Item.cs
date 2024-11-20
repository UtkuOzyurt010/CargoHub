namespace CargoHub.Models{
    public class Item
    {
        public string Uid { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Short_Description { get; set; }
        public string Upc_Code { get; set; }
        public string Model_Number { get; set; }
        public string Commodity_Code { get; set; }
        public int Item_Line { get; set; }
        public int Item_Group { get; set; }
        public int Item_Type { get; set; }
        public int Unit_Purchase_Quantity { get; set; }
        public int Unit_Order_Quantity { get; set; }
        public int Pack_Order_Quantity { get; set; }
        public int Supplier_Id { get; set; }
        public string Supplier_Code { get; set; }
        public string Supplier_Part_Number { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}