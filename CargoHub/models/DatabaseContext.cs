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
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(u => u.Uid); // Explicitly set Uid as the primary key
            });
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(n => n.Phone); // Explicitly set phone number as key
            });
            modelBuilder.Entity<Shipment>()
            .OwnsMany(s => s.Items, a =>
            {
                a.Property(i => i.Item_Id).HasColumnName("Item_Id");
                a.Property(i => i.Amount).HasColumnName("Amount");
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