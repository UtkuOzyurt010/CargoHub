using CargoHub.Models;
using CargoHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace CargoHub;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:8000");

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(40);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("TestSqliteDb")));

        // Register services
        builder.Services.AddTransient<IGenericService<Client>, ClientService>();
        builder.Services.AddTransient<IItemService, ItemService>();
        builder.Services.AddTransient<IGenericService<Inventory>, InventoryService>();
        builder.Services.AddTransient<IGenericService<ItemGroup>, ItemGroupService>();
        builder.Services.AddTransient<IGenericService<ItemLine>, ItemLineService>();
        builder.Services.AddTransient<IGenericService<ItemType>, ItemTypeService>();
        builder.Services.AddTransient<IGenericService<Location>, LocationService>();
        builder.Services.AddTransient<IGenericService<Order>, OrderService>();
        builder.Services.AddTransient<IGenericService<Shipment>, ShipmentService>();
        builder.Services.AddTransient<IGenericService<Supplier>, SupplierService>();
        builder.Services.AddTransient<IGenericService<Transfer>, TransferService>();
        builder.Services.AddTransient<IGenericService<Warehouse>, WarehouseService>();

        // temp
        builder.Services.AddScoped<MigrationService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var migrationService = scope.ServiceProvider.GetRequiredService<MigrationService>();
            await migrationService.MigrateAll();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
        return 0;
    }
}