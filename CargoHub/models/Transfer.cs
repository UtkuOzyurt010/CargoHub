namespace CargoHub.Models{
    public class Transfer
    {
        public int Id { get; set; }
        public required string Reference { get; set; }
        public int? TransferFrom { get; set; }
        public int TransferTo { get; set; }
        public required string TransferStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required List<Item> Items { get; set; }
    }
}