using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CargoHub.Models

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
        public DbSet<Contact> Contact { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // These tell the database to ignore the items list in Shipments (so we can insert json)
            modelBuilder.Entity<Shipment>()
            .Property(s => s.ItemsJson)
            .HasColumnName("ItemsJson");

            modelBuilder.Entity<Shipment>()
            .Ignore(s => s.Items);

            modelBuilder.Entity<Transfer>()
            .Property(s => s.ItemsJson)
            .HasColumnName("ItemsJson");

            modelBuilder.Entity<Transfer>()
            .Ignore(s => s.Items);

            modelBuilder.Entity<Order>()
            .Property(s => s.ItemsJson)
            .HasColumnName("ItemsJson");

            modelBuilder.Entity<Order>()
            .Ignore(s => s.Items);

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(u => u.Uid); // Explicitly set Uid as the primary key
            });
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(n => n.Phone); // Explicitly set phone number as key
            });

            modelBuilder.Entity<ItemGroup>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<ItemLine>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
            modelBuilder.Entity<ItemType>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        }
    }   
}