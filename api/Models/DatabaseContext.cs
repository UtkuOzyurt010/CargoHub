using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Models
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

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}