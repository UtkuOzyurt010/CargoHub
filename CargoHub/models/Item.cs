namespace CargoHub.Models{
    public class Item
    {
        public int Id {get; set;} //dotnet wants this here as a primary key.
        //System.InvalidOperationException: The entity type 'Item' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'
        //only adding Id as a temporary fix, we need to consider what to do with Uid/Id
        public string Uid { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string UpcCode { get; set; }
        public string ModelNumber { get; set; }
        public string CommodityCode { get; set; }
        public int ItemLine { get; set; }
        public int ItemGroup { get; set; }
        public int ItemType { get; set; }
        public int UnitPurchaseQuantity { get; set; }
        public int UnitOrderQuantity { get; set; }
        public int PackOrderQuantity { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierPartNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}