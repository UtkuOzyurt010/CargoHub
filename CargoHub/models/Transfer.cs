using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHub.Models{
    public class Transfer
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int? Transfer_From { get; set; }
        public int Transfer_To { get; set; }
        public string Transfer_Status { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        // Store items as JSON
        // I want the code to ignore the jsonString when responding and only return Items
        [JsonIgnore]
        public string ItemsJson { get; set; }

        // Transient property for easy manipulation of itemsjson
        [NotMapped]
        public List<TransferItem>? Items
        {
            get => string.IsNullOrEmpty(ItemsJson) 
                ? new List<TransferItem>() 
                : JsonConvert.DeserializeObject<List<TransferItem>>(ItemsJson);
            set => ItemsJson = JsonConvert.SerializeObject(value);
        }
    }

    public class TransferItem
    {
        public string Item_Id { get; set; }
        public int Amount { get; set; }
    }
}