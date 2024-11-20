//{"id": 0, "name": "Electronics", "description": "", "created_at": "1998-05-15 19:52:53", "updated_at": "2000-11-20 08:37:56"}

namespace CargoHub.Models{
    public class ItemGroup
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}