using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHub.Models{
    public class Order
    {
        public int Id { get; set; }
        public int Source_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public DateTime Request_Date { get; set; }
        public string? Reference { get; set; }
        public string? Reference_Extra { get; set; }
        public string? Order_Status { get; set; }
        public string? Notes { get; set; }
        public string? Shipping_Notes { get; set; }
        public string? Picking_Notes { get; set; }
        public int Warehouse_Id { get; set; }
        public int? Ship_To { get; set; }
        public int? Bill_To { get; set; }
        public int? Shipment_Id { get; set; }
        public decimal? Total_Amount { get; set; }
        public decimal? Total_Discount { get; set; }
        public decimal? Total_Tax { get; set; }
        public decimal? Total_Surcharge { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string ItemsJson { get; set; }

        // Transient property for easy manipulation of itemsjson
        [NotMapped]
        public List<OrderItem>? Items { get; set; }
    }

    public class OrderItem
    {
        public string Item_Id { get; set; }
        public int Amount { get; set; }
    }
}