namespace CargoHub.Models{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public Contact Contact { get; set; }
    }

    public class Contact
    {
        public int Id {get; set;} //dotnet wants this here as a primary key
        //System.InvalidOperationException: The entity type 'Contact' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
