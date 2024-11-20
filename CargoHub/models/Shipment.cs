namespace CargoHub.Models{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SourceId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public required string ShipmentType { get; set; }
        public required string ShipmentStatus { get; set; }
        public required string Notes { get; set; }
        public required string CarrierCode { get; set; }
        public required string CarrierDescription { get; set; }
        public required string ServiceCode { get; set; }
        public required string PaymentType { get; set; }
        public required string TransferMode { get; set; }
        public int TotalPackageCount { get; set; }
        public decimal TotalPackageWeight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required List<Item> Items { get; set; }
    }
}