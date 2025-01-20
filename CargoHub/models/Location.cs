namespace CargoHub.Models{
    public class Location
    {
        public int Id { get; set; }
        public int Warehouse_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}