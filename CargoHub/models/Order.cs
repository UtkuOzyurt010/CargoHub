namespace CargoHub.Models{
    public class Order
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequestDate { get; set; }
        public required string Reference { get; set; }
        public required string ReferenceExtra { get; set; }
        public required string OrderStatus { get; set; }
        public required string Notes { get; set; }
        public required string ShippingNotes { get; set; }
        public required string PickingNotes { get; set; }
        public int WarehouseId { get; set; }
        public required string ShipTo { get; set; }
        public required string BillTo { get; set; }
        public int ShipmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalSurcharge { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required List<Item> Items { get; set; }
    }
}