namespace CargoHub.Models{
    public class Warehouse
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Zip { get; set; }
        public required string City { get; set; }
        public required string Province { get; set; }
        public required string Country { get; set; }
        public required Contact Contact { get; set; }
    }

    public class Contact
    {
        public int Id {get; set;} //dotnet wants this here as a primary key
        //System.InvalidOperationException: The entity type 'Contact' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}
