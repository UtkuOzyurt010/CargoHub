using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemGroup> ItemGroup { get; set; }
        public DbSet<ItemLine> ItemLine { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Contact> Contact { get; set; } //used by Warehouse

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
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
        // }

    }

    
}