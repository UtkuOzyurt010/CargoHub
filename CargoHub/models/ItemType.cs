//{"id": 0, "name": "Laptop", "description": "", "created_at": "2001-11-02 23:02:40", "updated_at": "2008-07-01 04:09:17"}

namespace CargoHub.Models{
    public class ItemType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}