//{"id": 1, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262,
// "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "2015-02-19 16:08:24", "updated_at": "2015-09-26 06:37:56"}

namespace CargoHub.Models{
    public class Inventory
    {
        public int Id { get; set; }
        public string Item_Id { get; set; }
        public string Description { get; set; }
        public string Item_Reference { get; set; }
        public List<int> Locations { get; set; }
        public int Total_On_Hand { get; set; }
        public int Total_Expected { get; set; }
        public int  Total_Ordered { get; set; }
        public int Total_Allocated { get; set; }
        public int Total_Available { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }    }
}