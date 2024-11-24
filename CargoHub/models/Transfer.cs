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
        public string ItemsJson { get; set; }

        // Transient property for easy manipulation of itemsjson
        [NotMapped]
        public List<TransferItem>? Items { get; set;}
    }

    public class TransferItem
    {
        public string Item_Id { get; set; }
        public int Amount { get; set; }
    }
}