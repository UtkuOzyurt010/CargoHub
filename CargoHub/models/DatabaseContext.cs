using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CargoHub.Models

{
    public class DatabaseContext : DbContext
    {
        public required DbSet<Client> Client { get; set; }
        public required DbSet<Inventory> Inventory { get; set; }
        public required DbSet<Item> Item { get; set; }
        public required DbSet<ItemGroup> ItemGroup { get; set; }
        public required DbSet<ItemLine> ItemLine { get; set; }
        public required DbSet<ItemType> ItemType { get; set; }
        public required DbSet<Location> Location { get; set; }
        public required DbSet<Order> Order { get; set; }
        public required DbSet<Shipment> Shipment { get; set; }
        public required DbSet<Supplier> Supplier { get; set; }
        public required DbSet<Transfer> Transfer { get; set; }
        public required DbSet<Warehouse> Warehouse { get; set; }
        public required DbSet<Contact> Contact { get; set; } //used by Warehouse

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //     modelBuilder.Entity<Client>()
        //         .HasIndex(p => p.Id).IsUnique();

        //     // modelBuilder.Entity<Admin>()
        //     //     .HasData(new Admin { AdminId = 1, Email = "admin1@example.com", UserName = "admin1", Password = EncryptionHelper.EncryptPassword("password") });
        //     // modelBuilder.Entity<Admin>()
        //     //     .HasData(new Admin { AdminId = 2, Email = "admin2@example.com", UserName = "admin2", Password = EncryptionHelper.EncryptPassword("tooeasytooguess") });
        //     // modelBuilder.Entity<Admin>()
        //     //     .HasData(new Admin { AdminId = 3, Email = "admin3@example.com", UserName = "admin3", Password = EncryptionHelper.EncryptPassword("helloworld") });
        //     // modelBuilder.Entity<Admin>()
        //     //     .HasData(new Admin { AdminId = 4, Email = "admin4@example.com", UserName = "admin4", Password = EncryptionHelper.EncryptPassword("Welcome123") });
        //     // modelBuilder.Entity<Admin>()
        //     //     .HasData(new Admin { AdminId = 5, Email = "admin5@example.com", UserName = "admin5", Password = EncryptionHelper.EncryptPassword("Whatisapassword?") });
            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 1, // Use a unique ID
                Name = "Sample Client",
                Address = "poo poo pee pee", 
                City = "p", ContactEmail = "d", 
                ContactName = "a", Country = "de", 
                CreatedAt = DateTime.Today, 
                Province = "huh", UpdatedAt = 
                DateTime.Now, 
                Zip_code = "d"
            });

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        }
    }   
}