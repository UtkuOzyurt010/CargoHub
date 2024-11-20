namespace Models{
    public class Transfer
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int? TransferFrom { get; set; }
        public int TransferTo { get; set; }
        public string TransferStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Item> Items { get; set; }
    }
}