namespace CargoHub.Models{
    public class Location
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}