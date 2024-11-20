//{"id": 0, "name": "Tech Gadgets", "description": "", "created_at": "2022-08-18 07:05:25", "updated_at": "2023-05-15 15:44:28"}

namespace CargoHub.Models{
    public class ItemLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}