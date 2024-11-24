using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHub.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Source_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public DateTime Request_Date { get; set; }
        public DateTime Shipment_Date { get; set; }
        public string Shipment_Type { get; set; }
        public string Shipment_Status { get; set; }
        public string Notes { get; set; }
        public string Carrier_Code { get; set; }
        public string Carrier_Description { get; set; }
        public string Service_Code { get; set; }
        public string Payment_Type { get; set; }
        public string Transfer_Mode { get; set; }
        public int Total_Package_Count { get; set; }
        public decimal Total_Package_Weight { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Store items as JSON
        // I want the code to ignore the jsonString when responding and only return Items
        //[JsonIgnore]
        public string ItemsJson { get; set; }

        // Transient property for easy manipulation of itemsjson
        [NotMapped]
        public List<ShipmentItem>? Items
        {
            get => string.IsNullOrEmpty(ItemsJson) 
                ? new List<ShipmentItem>() 
                : JsonConvert.DeserializeObject<List<ShipmentItem>>(ItemsJson);
            set => ItemsJson = JsonConvert.SerializeObject(value);
        }
    }

    public class ShipmentItem
    {
        public string Item_Id { get; set; }
        public int Amount { get; set; }
    }
}