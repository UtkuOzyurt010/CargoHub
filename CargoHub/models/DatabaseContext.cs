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

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(n => n.Phone); // Explicitly set phone number as key
            });
            // Adding configuration for Item entity
            modelBuilder.Entity<Item>(entity =>
            {
                // Make sure Id is auto-incrementing and is the primary key
                entity.HasKey(i => i.Id);  // Primary key configuration
                entity.Property(i => i.Id)
                    .ValueGeneratedOnAdd(); // Auto-increment behavior

                // If you want to make sure Uid is a regular column, just add it here
                entity.Property(i => i.Uid).HasColumnName("Uid");

                // Other configurations for Item can go here
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        }
    }   
}